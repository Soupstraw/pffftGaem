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
		float rotation = transform.eulerAngles.z / 180 * Mathf.PI;
		direction = new Vector3(Mathf.Cos(rotation), Mathf.Sin(rotation), 0) * speed;
		attacker = gameObject.GetComponent<Attacker>();
	}

	void Update () {
		transform.Translate (direction);
	}

	void OnTriggerEnter2D(Collider2D other) {
		attacker.DealDamage ();
		Destroy (gameObject);
	}
}
