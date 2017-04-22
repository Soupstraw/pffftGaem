﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : MonoBehaviour {

	public float damage;

	private Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
	}
	
	public void DealDamage(){
		ContactFilter2D filter = new ContactFilter2D ();
		filter.NoFilter ();
		Collider2D[] res = new Collider2D[100];
		Debug.Log(Physics2D.OverlapCollider (collider, filter, res));
		foreach(Collider2D col in res){
			if (col != null) {
				Damagable dam = col.GetComponent<Damagable> ();
				if (dam != null) {
					dam.DealDamage (damage);
				}
			}
		}
	}
}
