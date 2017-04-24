using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Aggro : MonoBehaviour {

	private bool triggered = false;

	private Collider2D coll;
	private AudioSource audio;

	public AudioClip[] chargeScream;

	// Use this for initialization
	void Start () {
		coll = GetComponent<Collider2D> ();
		audio = GetComponent<AudioSource> ();
		SetAIEnabled (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Player"){
			SetAIEnabled (true);
			if(audio != null && chargeScream != null && chargeScream.Length > 0){
				if (!triggered) {
					audio.PlayOneShot (chargeScream [Random.Range (0, chargeScream.Length)]);
					triggered = true;
				}
			}
		}
	}

	void SetAIEnabled(bool enable){
		ContactFilter2D filter = new ContactFilter2D ();
		LayerMask mask = LayerMask.GetMask ("Enemy");
		filter.SetLayerMask (mask);
		Collider2D[] res = new Collider2D[100];
		Physics2D.OverlapCollider (coll, filter, res);
		foreach(Collider2D col in res){
			if (col != null) {
				AI ai = col.GetComponent<AI> ();
				if (ai != null) {
					ai.enabled = enable;
				}
			}
		}
	}
}
