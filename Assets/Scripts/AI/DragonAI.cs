using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class DragonAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool attackNoCD = true;
	bool casting = false;

	public float maxAttackDistance = 15.0f;
	public float kiteDistance = 10.0f;
	public float minAttackDistance = 5.0f;
	public float attackCooldown = 5.0f;
	public float castTime = 1.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
	}

	//Stays at optimal distance, only fires between two distances.
	void Update () {
		if (!casting && ai.enabled) {
			float distance = Vector3.Distance (transform.position, player.transform.position);
			ai.KeepDistance (kiteDistance);
			//Pause to fire.
			if (attackNoCD && distance < maxAttackDistance && distance > minAttackDistance) {
				ai.Stop ();
				casting = true;
				StartCoroutine (CastCooldown ());
				attackNoCD = false;
				StartCoroutine (Cooldown ());
			}
		}
	}

	IEnumerator Cooldown() {
		yield return new WaitForSeconds (attackCooldown);
		attackNoCD = true;
	}

	IEnumerator CastCooldown() {
		yield return new WaitForSeconds (castTime * 0.8f);
		ai.BasicAttack (ai.firstAttack);
		yield return new WaitForSeconds (castTime * 0.2f);
		casting = false;
	}

}
