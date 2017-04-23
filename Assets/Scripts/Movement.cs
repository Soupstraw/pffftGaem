using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {
	
	public float speed = 5.0f;

	public Vector3 lastNotZero = new Vector3();

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Attack atk = GetComponentInChildren<Attack> ();

		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
		Vector3 animDir = lastNotZero;
		if (moveDir.magnitude > 0.5f) {
			lastNotZero = moveDir * 0.1f;
			animDir = moveDir;
		}
		GetComponent<Rigidbody2D>().velocity =  moveDir * speed * atk.movementMultiplier;

		Vector3 attackDir = atk.attackDir;
		if (moveDir.magnitude > 0 || atk.charging) {
			GetComponent<Animator> ().SetFloat ("XMovement", attackDir.x);
			GetComponent<Animator> ().SetFloat ("YMovement", attackDir.y);
		} else {
			GetComponent<Animator> ().SetFloat ("XMovement", attackDir.x * .1f);
			GetComponent<Animator> ().SetFloat ("YMovement", attackDir.y * .1f);
		}
	}
}
