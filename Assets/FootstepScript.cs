using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepScript : MonoBehaviour {

	private AudioSource source;

	public AudioClip[] footsteps;

	void Start(){
		source = GetComponent<AudioSource> ();
	}

	public void PlayFootstepSound(){
		source.PlayOneShot (footsteps[Random.Range(0, footsteps.Length)]);
	}
}
