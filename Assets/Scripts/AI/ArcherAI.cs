using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class ArcherAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool attackNoCD = true;

	public float attackDistance = 15.0f;
	public float kiteDistance = 10.0f;
	public float attackCooldown = 5.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		ai.KeepDistance(kiteDistance);
		if (attackNoCD && Vector3.Distance (transform.position, player.transform.position) < attackDistance) {
			ai.BasicAttack ();
			attackNoCD = false;
			StartCoroutine (Cooldown());
		}
	}

	IEnumerator Cooldown() {
		yield return new WaitForSeconds (attackCooldown);
		attackNoCD = true;
	}

}
