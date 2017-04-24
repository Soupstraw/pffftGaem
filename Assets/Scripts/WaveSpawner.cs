using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	public GameObject dummy, bandit, archer, knight, rogue;
	public float simpleSpawnX, simpleSpawnY, spawnBoundaryX, spawnBoundaryY;
	int waveNumber = 0;

	public IEnumerator DummyRush() {
		float startTime = Time.time;
		while (true) {
			Instantiate(dummy, transform.position + SimpleSpawn, Quaternion.identity);
			yield return new WaitForSeconds(300 / (60 + Time.time - startTime));
		}
	}

	Vector3 SimpleSpawn {
		get {
			return new Vector3(simpleSpawnX * Random.value, simpleSpawnY * Random.value);
		}
	}

	Vector3 AreaSpawn {
		get {
			if (Random.Range(0, 2) == 0) {
				return new Vector3(spawnBoundaryX * (Random.Range(0, 2) - 0.5f), spawnBoundaryY * Random.Range(-0.5f, 0.5f));
			} else {
				return new Vector3(spawnBoundaryX * Random.Range(-0.5f, 0.5f), spawnBoundaryY * (Random.Range(0, 2) - 0.5f));
			}
		}
	}

}
