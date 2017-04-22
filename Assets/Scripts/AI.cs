using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	GameObject player;
	Rigidbody2D rb;

	public float speed = 0.1f;
	public GameObject meleeAttack;

	void Start () {
		player = GameObject.FindWithTag("Player");
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		KeepDistance (2);
		if (Time.time % 5 < 0.1) {
			MeleeAttack ();
		}
	}

	void Approach() { //Beeline
		Vector3 direction = player.transform.position - transform.position;
		rb.velocity = new Vector2(direction.x, direction.y).normalized;
	}

	void KeepDistance(float distance) {
		Vector3 reverseDirection = (transform.position - player.transform.position);
		reverseDirection.z = 0;
		reverseDirection = reverseDirection.normalized * distance + player.transform.position - transform.position;
		rb.velocity = new Vector2 (reverseDirection.x, reverseDirection.y).normalized;
	}

	void MeleeAttack() { //Spawns attack 1 unit away, needs tweaking.
		Vector3 direction = (player.transform.position - transform.position).normalized + transform.position;
		Quaternion rotation = Quaternion.identity;
		float rot = (direction.y / direction.x) / Mathf.PI * 180;
		if (direction.x < 0) {
			rot += 180;
		}
		rotation.eulerAngles = new Vector3 (0, 0, rot);
		Instantiate (meleeAttack, direction, rotation);
	}

}
