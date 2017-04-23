using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour {
	Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
	}

	void Update () {
		Vector3 difference = startPosition - transform.position;
		Vector2 diff = new Vector2(Mathf.Clamp(1/difference.x, -1, 1), Mathf.Clamp(1/difference.y, -1, 1));
		if (diff.x < 0) {
			diff.x = -1 - diff.x;
		} else {
			diff.x = 1 - diff.x;
		}
		if (diff.y < 0) {
			diff.y = -1 - diff.y;
		} else {
			diff.y = 1 - diff.y;
		}
		transform.Translate ((Random.insideUnitCircle.normalized + diff) * Time.deltaTime);
	}
}
