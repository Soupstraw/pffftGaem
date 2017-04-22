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

	public void DoCharge(){
		StartCoroutine(ChargeCoroutine(speed, time));
	}

	IEnumerator ChargeCoroutine(float speed, float time){
		float startTime = Time.time;
		while(Time.time - startTime < time){
			rigid.velocity = Quaternion.Euler(0, 0, reticule.rotation.eulerAngles.z) * Vector3.down * (time - Time.time + startTime) * speed;
			yield return null;
		}
	}
}
