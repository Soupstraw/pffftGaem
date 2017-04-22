using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class RogueAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool meleeNoCD = true;
	bool rangedNoCD = true;

	public float attackDistance = 7.5f;
	public float kiteDistance = 5.0f;
	public float aggroDistance = 3.0f;
	public float meleeCooldown = 5.0f;
	public float rangedCooldown = 3.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
	}

	//Stays at medium range, if the player gets too close, stabs them and jumps away.
	void Update () {
		float distance = Vector3.Distance (transform.position, player.transform.position);
		if (meleeNoCD && distance < aggroDistance) {

		}
		ai.KeepDistance(kiteDistance);
		if (rangedNoCD && distance < attackDistance) {
			ai.BasicAttack (ai.firstAttack);
			rangedNoCD = false;
			StartCoroutine (RangedCooldown());
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

}