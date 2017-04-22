using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour {

	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
		GetComponent<Rigidbody2D>().velocity =  moveDir * speed;
		GetComponent<Animator> ().SetFloat ("XMovement", moveDir.x);
		GetComponent<Animator> ().SetFloat ("YMovement", moveDir.y);
	}
}
