using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Damagable : MonoBehaviour {

	public float health = 100f;
	public float healthMax = 100f;

	public GameObject splatter;

	private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
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

	public void Knockback(Vector3 dir){
		StartCoroutine (KnockbackCoroutine (dir));
	}

	IEnumerator KnockbackCoroutine(Vector3 dir){
		AI ai = GetComponent<AI> ();
		if (ai != null) {
			ai.enabled = false;
		}

		float startTime = Time.time;
		while(Time.time - startTime < dir.magnitude){
			rigid.velocity = dir.normalized * (dir.magnitude - Time.time + startTime) * dir.magnitude;
			yield return null;
		}

		if (ai != null) {
			ai.enabled = true;
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
