using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
	GameObject parent;
	public float timer = 1;

	void Start () {
		Destroy (gameObject, timer);
	}
}
