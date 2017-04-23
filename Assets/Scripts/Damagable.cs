﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public float health = 100f;
	public float healthMax = 100f;

	public GameObject splatter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DealDamageOverTime(float damage, int time){
		StartCoroutine (DamageCoroutine(damage, time));
	}

	public void DealDamage(float damage){
		health -= damage;
		Debug.Log ("Health: " + health);
		if(splatter != null){
			GameObject splat = Instantiate (splatter, transform.position, Quaternion.Euler(0, 0, Random.value * 360f));;
			splat.transform.localScale *= damage / healthMax;
		}
		if (health <= 0) {
			Kill ();
		}
	}

	public void Kill(){
		if (splatter != null) {
			Instantiate (splatter, transform.position, Quaternion.Euler(0, 0, Random.value * 360f));
		}
		Destroy (gameObject);
	}

	IEnumerator DamageCoroutine(float damage, int time){
		for(int i = 0; i < time; i++){
			DealDamage (damage / time);
			yield return new WaitForSeconds (1.0f);
		}
	}
}
