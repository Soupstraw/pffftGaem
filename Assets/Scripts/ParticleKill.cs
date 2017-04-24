using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleKill : MonoBehaviour {

	private ParticleSystem part;

	// Use this for initialization
	void Start () {
		part = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (part.isStopped) {
			Destroy (gameObject);
		}
	}
}
