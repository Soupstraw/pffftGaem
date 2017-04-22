﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	GameObject player;
	Rigidbody2D rb;

	public float speed = 0.1f;
	public GameObject firstAttack;
	public GameObject secondAttack;

	void Start () {
		player = GameObject.FindWithTag("Player");
		rb = gameObject.GetComponent<Rigidbody2D> ();
	}

	public void Beeline() {
		Vector3 direction = player.transform.position - transform.position;
		if (Vector3.Magnitude (direction) < 0.1) {
			rb.velocity = new Vector2 (direction.x, direction.y).normalized;
		} else {
			rb.velocity = Vector2.zero;
		}
	}

	public void KeepDistance(float distance) {
		Vector3 reverseDirection = (transform.position - player.transform.position);
		if (Vector3.Magnitude (reverseDirection) < 0.1) {
			reverseDirection.z = 0;
			reverseDirection = reverseDirection.normalized * distance + player.transform.position - transform.position;
			rb.velocity = new Vector2 (reverseDirection.x, reverseDirection.y).normalized;
		} else {
			rb.velocity = Vector2.zero;
		}
	}

	public void BasicAttack(GameObject projectile) {
		Vector3 direction = (player.transform.position - transform.position).normalized;
		Quaternion rotation = Quaternion.identity;
		float rot = Mathf.Atan(direction.y / direction.x) / Mathf.PI * 180;
		if (direction.x < 0) {
			rot += 180;
		}
		rotation.eulerAngles = new Vector3 (0, 0, rot);
		Instantiate (projectile, transform.position, rotation);
	}

	public void Evade(Vector3 direction, float length) {
		StartCoroutine (Dodge(direction, length));
	}

	IEnumerator Dodge(Vector3 direction, float length) {
		Vector3 startPosition = transform.position;
		Vector3 endPosition = startPosition + direction * length;
		while (Vector3.Distance (startPosition, endPosition) < length) {
			rb.velocity = direction.normalized;
			yield return null;
		}
	}

}
