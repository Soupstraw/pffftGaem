using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	GameObject player;
	public float speed = 0.1f;
	Rigidbody2D rb;

	void Start () {
		player = GameObject.FindWithTag("Player");
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update () {
		Approach ();
	}

	void Approach() {
		Vector3 direction = player.transform.position - transform.position;
		direction.z = 0;
		direction.Normalize ();
		//float rotation;
		//rotation = (direction.y / direction.x) / Mathf.PI * 180;
		//if (direction.x < 0) {
		//	rotation += 180;
		//}
		rb.velocity = new Vector2(direction.x, direction.y).normalized;
	}


}
