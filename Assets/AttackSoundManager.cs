using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttackSoundManager : MonoBehaviour {

	private AudioSource source;

	public AudioClip slamSound;
	public AudioClip lightAttack;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	public void PlaySlamSound(){
		source.PlayOneShot (slamSound);
	}

	public void PlayLightAttack(){
		source.PlayOneShot (lightAttack);
	}
}
