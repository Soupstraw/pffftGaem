using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class DummyAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool attackNoCD = true;

	public float attackDistance = 1.0f;
	public float attackCooldown = 5.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
	}

	//Rushes to attack.
	void Update () {
		if (ai.enabled) {
			ai.KeepDistance (2.0f);
			if (attackNoCD && Vector3.Distance (transform.position, player.transform.position) < attackDistance) {
				ai.BasicAttack (ai.firstAttack);
				attackNoCD = false;
				StartCoroutine (Cooldown ());
			}
		}
	}

	IEnumerator Cooldown() {
		yield return new WaitForSeconds (attackCooldown);
		attackNoCD = true;
	}

}
