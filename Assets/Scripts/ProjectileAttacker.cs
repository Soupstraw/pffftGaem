using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class ProjectileAttacker : MonoBehaviour {
	public float timeout;
	public float speed;
	Vector3 direction;

	Attacker attacker;

	void Start () {
		Destroy (gameObject, timeout);
		attacker = gameObject.GetComponent<Attacker>();
	}

	void Update () {
		transform.Translate (Vector3.right * speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			attacker.DealDamage ();
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Player") {
			attacker.DealDamage ();
			Destroy (gameObject);
		}
	}
}
