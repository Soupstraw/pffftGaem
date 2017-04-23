using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class BanditAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool attackNoCD = true;
	bool evading = false;

	public float evadeTime = 2f;
	public float attackDistance = 1.0f;
	public float attackCooldown = 5.0f;

	void Start() {
		ai = gameObject.GetComponent<AI> ();
		player = GameObject.FindWithTag("Player");
	}

	void OnEnable() {
		Attacker.OnAttack += TryEvade;
	}

	void OnDisable() {
		Attacker.OnAttack -= TryEvade;
	}

	//Rushes to attack.
	void Update () {
		if (!evading && ai.enabled) {
			ai.Beeline ();
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

	IEnumerator EvadeCooldown() {
		yield return new WaitForSeconds (evadeTime);
		evading = false;
	}

	void TryEvade(Attacker damage) {
		GameObject attack = damage.gameObject;
		if (attack.name == "SlamAttack") {
			Vector3 radius = transform.position - attack.transform.position;
			float dodgeDistance = attack.transform.localScale.x / 2 + 1 - Vector3.Magnitude (radius);
			if (dodgeDistance > 0) {
				radius = radius.normalized * (dodgeDistance);
				StartCoroutine(ai.Evade (radius, dodgeDistance / evadeTime, evadeTime));
				evading = true;
				StartCoroutine (EvadeCooldown ());
			}
		} else if (attack.name == "HeavyAttack") {
		}
	}

}
