using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : MonoBehaviour {
	Vector3 startPosition;
	public int inverseWobbleFactor = 2;
	public float wanderDistance = 1;
	int counter = 0;
	Vector2 diff = Vector2.zero;

	void Start () {
		startPosition = transform.position;
	}

	void Update () {
		if (counter == 0) {
			Vector3 difference = startPosition - transform.position;
			diff = new Vector2 (Mathf.Clamp (1 / difference.x, -1, 1), Mathf.Clamp (1 / difference.y, -1, 1));
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
			diff /= wanderDistance;
			diff += Random.insideUnitCircle.normalized;
		}
		counter++;
		if (counter >= inverseWobbleFactor) {
			counter = 0;
		}
		transform.Translate (diff * Time.deltaTime);
	}
}
