using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	public AudioSource primarySource;
	public AudioSource secondarySource;

	public float crossFadeTime = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeSong(AudioClip song){
		if (!secondarySource.isPlaying) {
			secondarySource.clip = song;
			StartCoroutine (CrossfadeCoroutine (true));
		} else if(!primarySource.isPlaying){
			primarySource.clip = song;
			StartCoroutine (CrossfadeCoroutine (false));
		}
	}

	IEnumerator CrossfadeCoroutine(bool primary){
		AudioSource a = primarySource, b = secondarySource;
		if (!primary) {
			a = secondarySource;
			b = primarySource;
		}
		b.Play ();
		float startTime = Time.time;
		while (Time.time - startTime < crossFadeTime) {
			a.volume = Mathf.Lerp (1, 0, (Time.time - startTime) / crossFadeTime);
			b.volume = Mathf.Lerp (0, 1, (Time.time - startTime) / crossFadeTime);
			yield return null;
		}
		a.Stop ();
	}
}
