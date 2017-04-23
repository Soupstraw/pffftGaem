﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {

	public float speed = 5.0f;

	private Vector3 lastNotZero = new Vector3();

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
		Vector3 animDir = lastNotZero;
		if (moveDir.magnitude > 0.5f) {
			lastNotZero = moveDir * 0.1f;
			animDir = moveDir;
		}
		GetComponent<Rigidbody2D>().velocity =  moveDir * speed;

		GetComponent<Animator> ().SetFloat ("XMovement", animDir.x);
		GetComponent<Animator> ().SetFloat ("YMovement", animDir.y);
	}
}
