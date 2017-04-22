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

	public void Beeline() {
		Vector3 direction = player.transform.position - transform.position;
		rb.velocity = new Vector2(direction.x, direction.y).normalized;
	}

	public void KeepDistance(float distance) {
		Vector3 reverseDirection = (transform.position - player.transform.position);
		reverseDirection.z = 0;
		reverseDirection = reverseDirection.normalized * distance + player.transform.position - transform.position;
		rb.velocity = new Vector2 (reverseDirection.x, reverseDirection.y).normalized;
	}

	public void BasicAttack() {
		Vector3 direction = (player.transform.position - transform.position).normalized;
		Quaternion rotation = Quaternion.identity;
		float rot = Mathf.Atan(direction.y / direction.x) / Mathf.PI * 180;
		if (direction.x < 0) {
			rot += 180;
		}
		rotation.eulerAngles = new Vector3 (0, 0, rot);
		Instantiate (meleeAttack, transform.position, rotation);
	}

}
