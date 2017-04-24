using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

	public AudioClip song;

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player") {
			FindObjectOfType<Music> ().ChangeSong (song);
		}
	}
}
