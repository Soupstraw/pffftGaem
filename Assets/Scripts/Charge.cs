using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Charge : MonoBehaviour {

	public float speed = 5f;
	public float time = 1f;

	public Transform reticule;

	private Rigidbody2D rigid;
	private Animator anim;

	void Start(){
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	public void DoCharge(Vector3 direction){
		StartCoroutine(ChargeCoroutine(speed, time, direction));
	}

	IEnumerator ChargeCoroutine(float speed, float time, Vector3 dir){
		anim.SetBool ("Dash", true);
		float startTime = Time.time;
		while(Time.time - startTime < time){
			rigid.velocity = dir.normalized * (time - Time.time + startTime) * speed;
			yield return null;
		}
		anim.SetBool ("Dash", false);
	}
}
