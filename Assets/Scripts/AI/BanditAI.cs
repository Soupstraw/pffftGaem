using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class BanditAI : MonoBehaviour {
	GameObject player;
	Attack attacks;
	AI ai;
	bool attackNoCD = true;

	public float attackDistance = 1.0f;
	public float attackCooldown = 5.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
		attacks = player.GetComponent<Attack> ();
	}

	void OnEnable() {
		attacks.OnSlamAttack += TryEvade;
	}

	void OnDisable() {
		attacks.OnSlamAttack -= TryEvade;
	}

	//Rushes to attack.
	void Update () {
		ai.Beeline();
		if (attackNoCD && Vector3.Distance (transform.position, player.transform.position) < attackDistance) {
			ai.BasicAttack (ai.firstAttack);
			attackNoCD = false;
			StartCoroutine (Cooldown());
		}
	}

	IEnumerator Cooldown() {
		yield return new WaitForSeconds (attackCooldown);
		attackNoCD = true;
	}

	void TryEvade(Collider2D collider) {

	}

}
