using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class KnightAI : MonoBehaviour, EvasiveAI {
	GameObject player;
	AI ai;
	bool attackNoCD = true;
	bool evading = false;
	bool stunned = false;

	public float stunTime = 5f;
	public float evadeTime = 2f;
	public float attackDistance = 1.0f;
	public float attackCooldown = 5.0f;

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

	//Rushes to attack.
	void Update () {
		if (!evading && !stunned && ai.enabled) {
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

	public IEnumerator EvadeCooldown() {
		yield return new WaitForSeconds (evadeTime);
		evading = false;
	}

	public IEnumerator StunCooldown() {
		yield return new WaitForSeconds (stunTime);
		stunned = false;
	}

	public void TryLocalEvade(Attacker damage) {
		if (!ai.enabled || stunned) {
			return;
		}
		GameObject attack = damage.gameObject;
		if (attack.name == "SlamAttack" || attack.name == "SpinAttack" || attack.name == "TurnAttack") {
			Vector3 radius = transform.position - attack.transform.position;
			float dodgeDistance = attack.transform.localScale.x / 2 + 1.5f - Vector3.Magnitude (radius);
			if (dodgeDistance > 0) {
				radius = radius.normalized * (dodgeDistance);
				StartCoroutine (ai.Evade (radius, 0f, evadeTime));
				evading = true;
				StartCoroutine (EvadeCooldown ());
				if (attack.name == "SlamAttack") {
					stunned = true;
					StartCoroutine (StunCooldown ());
				}
			}
		}
	}

	public void TryEvade(GameObject attack) {
		if (!ai.enabled || stunned) {
			return;
		}
		float evadeRotation = (attack.transform.eulerAngles.z + Random.Range (0, 2) * 180) / 180 * Mathf.PI;
		Vector3 evadeDirection = new Vector3(Mathf.Cos(evadeRotation), Mathf.Sin(evadeRotation), 0);
		StartCoroutine (ai.Evade (evadeDirection, 0f, evadeTime));
		evading = true;
		StartCoroutine (EvadeCooldown ());
		if (attack.name == "HeavyAttack") {
			stunned = true;
			StartCoroutine (StunCooldown ());
		}
	}
}
