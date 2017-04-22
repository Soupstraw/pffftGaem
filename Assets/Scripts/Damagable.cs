using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public float health = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DealDamage(float damage){
		health -= damage;
		Debug.Log ("Health: " + health);
		if (health <= 0) {
			Kill ();
		}
	}

	public void Kill(){
		Destroy (gameObject);
	}
}
