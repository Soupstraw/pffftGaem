using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class BanditAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool attackNoCD = true;
	bool evading = false;

	public float evadeTime = 0.25f;
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
			if (Vector3.Magnitude (radius) < attack.transform.localScale.x / 2 + 1) {
				StartCoroutine(ai.Evade (radius, (Vector3.Magnitude (radius) + 1) / evadeTime, evadeTime));
				evading = true;
				StartCoroutine (EvadeCooldown ());
			}
		} else if (attack.name == "HeavyAttack") {
		}
	}

}
