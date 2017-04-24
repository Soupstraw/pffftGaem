using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
public class DragonAI : MonoBehaviour {
	GameObject player;
	AI ai;
	bool attackNoCD = true;
	bool casting = false;
	bool evading = false;

	public float evadeTime = 2f;
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
		if (!casting && !evading && ai.enabled) {
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
