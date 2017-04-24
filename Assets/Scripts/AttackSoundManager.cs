using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttackSoundManager : MonoBehaviour {

	private AudioSource source;

	public AudioClip slamSound;
	public AudioClip lightAttack;
	public AudioClip spinAttack;
	public AudioClip turnAttack;

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

	public void PlaySpinAttack(){
		source.PlayOneShot (spinAttack);
	}

	public void PlayTurnAttack(){
		source.PlayOneShot (turnAttack);
	}
}
