using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class RogueAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool meleeNoCD = true;
	bool rangedNoCD = true;
	bool evading = false;

	public float attackDistance = 7.5f;
	public float kiteDistance = 5.0f;
	public float aggroDistance = 3.0f;
	public float evadeTime = 0.25f;
	public float meleeCooldown = 5.0f;
	public float rangedCooldown = 3.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
	}

	//Stays at medium range, if the player gets too close, stabs them and jumps away.
	void Update () {
		if (!evading && ai.enabled) {
			float distance = Vector3.Distance (transform.position, player.transform.position);
			if (meleeNoCD && distance < aggroDistance) { //If player is close, do the evade combo.
				Vector3 evadeDirection = player.transform.position - transform.position;
				StartCoroutine (ai.Evade (evadeDirection, aggroDistance / evadeTime, evadeTime));
				evading = true;
				StartCoroutine (EvadeCooldown (evadeDirection));
				meleeNoCD = false;
				StartCoroutine (MeleeCooldown ());
			} else {
				ai.KeepDistance (kiteDistance);
				if (rangedNoCD && distance < attackDistance) {
					ai.BasicAttack (ai.firstAttack);
					rangedNoCD = false;
					StartCoroutine (RangedCooldown ());
				}
			}
		}
	}

	IEnumerator MeleeCooldown() {
		yield return new WaitForSeconds (meleeCooldown);
		meleeNoCD = true;
	}

	IEnumerator RangedCooldown() {
		yield return new WaitForSeconds (rangedCooldown);
		rangedNoCD = true;
	}

	IEnumerator EvadeCooldown(Vector3 direction) {
		yield return new WaitForSeconds (evadeTime);
		ai.BasicAttack (ai.secondAttack);
		StartCoroutine (ai.Evade (direction, aggroDistance / evadeTime, evadeTime));
		yield return new WaitForSeconds (evadeTime);
		evading = false;
	}

}