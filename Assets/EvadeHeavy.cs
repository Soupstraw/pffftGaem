using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI))]
[RequireComponent(typeof(BanditAI))]
public class EvadeHeavy : MonoBehaviour {
	BanditAI banditAI;

	void Start() {
		banditAI = gameObject.GetComponent<BanditAI> ();
	}

	void TryEvade(GameObject attack) {
		if (!banditAI.ai.enabled) {
			return;
		}
		float evadeRotation = (attack.transform.eulerAngles.z + Random.Range (0, 2) * 180 - 90) / 180 * Mathf.PI;
		Vector3 evadeDirection = new Vector3(Mathf.Cos(evadeRotation), Mathf.Sin(evadeRotation), 0);
		StartCoroutine (banditAI.ai.Evade (evadeDirection, 2.0f / banditAI.evadeTime, banditAI.evadeTime));
		banditAI.evading = true;
		StartCoroutine (banditAI.EvadeCooldown ());
	}
}
