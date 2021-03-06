using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class BanditAI : MonoBehaviour, EvasiveAI {
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
		Attacker.OnAttack += TryLocalEvade;
	}

	void OnDisable() {
		Attacker.OnAttack -= TryLocalEvade;
	}

	//Rushes to attack.
	void Update () {
		if (!evading && ai.enabled) {
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

	public void TryLocalEvade(Attacker damage) {
		GameObject attack = damage.gameObject;
		if (ai.enabled && attack.name == "SlamAttack") {
			Vector3 radius = transform.position - attack.transform.position;
			float dodgeDistance = attack.transform.localScale.x / 2 + 4 - Vector3.Magnitude (radius);
			if (dodgeDistance > 0) {
				radius = radius.normalized * (dodgeDistance);
				StartCoroutine(ai.Evade (radius, dodgeDistance / evadeTime, evadeTime));
				evading = true;
				StartCoroutine (EvadeCooldown ());
			}
		}
	}

	public void TryEvade(GameObject attack) {
		if (!ai.enabled || attack.name == "LightAttack") {
			return;
		}
		float evadeRotation = (attack.transform.eulerAngles.z + Random.Range (0, 2) * 180) / 180 * Mathf.PI;
		Vector3 evadeDirection = new Vector3(Mathf.Cos(evadeRotation), Mathf.Sin(evadeRotation), 0);
		StartCoroutine (ai.Evade (evadeDirection, 2.0f / evadeTime, evadeTime));
		evading = true;
		StartCoroutine (EvadeCooldown ());
	}
}
