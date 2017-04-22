using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Charge : MonoBehaviour {

	public float speed = 5f;
	public float time = 1f;

	public Transform reticule;

	private Rigidbody2D rigid;

	void Start(){
		rigid = GetComponent<Rigidbody2D> ();
	}

	public void DoCharge(Vector3 direction){
		StartCoroutine(ChargeCoroutine(speed, time, direction));
	}

	IEnumerator ChargeCoroutine(float speed, float time, Vector3 dir){
		float startTime = Time.time;
		while(Time.time - startTime < time){
			rigid.velocity = dir.normalized * (time - Time.time + startTime) * speed;
			yield return null;
		}
	}
}
