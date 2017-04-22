using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Attack : MonoBehaviour {

	private Animator anim;

	public GameObject reticule;

	public Attacker lightAttack;
	public Attacker heavyAttack;

	public void LightAttack(){
		lightAttack.DealDamage ();
	}

	public void HeavyAttack(){
		heavyAttack.DealDamage ();
	}

	void Start(){
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("LightAttack")) {
			anim.SetTrigger ("LightAttack");
		}
		if (Input.GetButtonDown ("HeavyAttack")) {
			anim.SetTrigger ("HeavyAttack");
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsTag("Movement")) {
			reticule.transform.rotation = Quaternion.Euler (60, 0, 0) * Quaternion.FromToRotation (Vector3.down, Input.mousePosition - new Vector3 (Screen.width / 2, Screen.height / 2));
		}
	}
}
