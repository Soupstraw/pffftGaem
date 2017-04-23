using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class RogueAI : MonoBehaviour, EvasiveAI {
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

	void OnEnable() {
		Attacker.OnAttack += TryLocalEvade;
	}

	void OnDisable() {
		Attacker.OnAttack -= TryLocalEvade;
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

	public IEnumerator EvadeCooldown2() {
		yield return new WaitForSeconds (evadeTime);
		evading = false;
	}

	public void TryLocalEvade(Attacker damage) {
		GameObject attack = damage.gameObject;
		if (ai.enabled && attack.name == "SlamAttack") {
			Vector3 radius = transform.position - attack.transform.position;
			float dodgeDistance = attack.transform.localScale.x / 2 + 2 - Vector3.Magnitude (radius);
			if (dodgeDistance > 0) {
				radius = radius.normalized * (dodgeDistance);
				StartCoroutine(ai.Evade (radius, dodgeDistance / evadeTime, evadeTime));
				evading = true;
				StartCoroutine (EvadeCooldown2 ());
			}
		}
	}

	public void TryEvade(GameObject attack) {
		if (!ai.enabled) {
			return;
		}
		float evadeRotation = (attack.transform.eulerAngles.z + Random.Range (0, 2) * 180) / 180 * Mathf.PI;
		Vector3 evadeDirection = new Vector3(Mathf.Cos(evadeRotation), Mathf.Sin(evadeRotation), 0);
		StartCoroutine (ai.Evade (evadeDirection, 2.0f / evadeTime, evadeTime));
		evading = true;
		StartCoroutine (EvadeCooldown2 ());
	}

}