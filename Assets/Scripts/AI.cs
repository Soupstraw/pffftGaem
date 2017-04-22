using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	GameObject player;
	public float speed = 0.1f;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		Approach ();
	}

	void Approach() {
		Vector3 direction = player.transform.eulerAngles - transform.eulerAngles;
		direction.z = 0;
		//float rotation;
		//rotation = (direction.y / direction.x) / Mathf.PI * 180;
		//if (direction.x < 0) {
		//	rotation += 180;
		//}
		transform.Translate (transform.forward * speed);
	}


}
