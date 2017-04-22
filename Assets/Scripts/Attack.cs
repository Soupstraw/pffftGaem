﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Attack : MonoBehaviour {

	public delegate void AttackAction(Collider2D collider);
	public event AttackAction OnHeavyAttack;
	public event AttackAction OnLightAttack;


	private Animator anim;

	public float rotationSpeed = 10f;

	public GameObject reticule;

	public Attacker lightAttack;
	public Attacker heavyAttack;
	public Attacker slamAttack;

	public void LightAttack(){
		lightAttack.DealDamage ();
	}

	public void HeavyAttack(){
		heavyAttack.DealDamage ();
	}

	public void SlamAttack(){
		slamAttack.DealDamage ();
	}

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void Charge(){
		GetComponent<Charge> ().DoCharge (Quaternion.Euler (0, 0, reticule.transform.rotation.eulerAngles.z) * Vector3.down);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("LightAttack")) {
			anim.SetTrigger ("LightAttack");
			if(OnLightAttack != null)
				OnLightAttack (lightAttack.GetComponent<Collider2D>());
		}
		if (Input.GetButtonDown ("HeavyAttack")) {
			anim.SetTrigger ("HeavyAttack");
			if(OnHeavyAttack != null)
				OnHeavyAttack (heavyAttack.GetComponent<Collider2D>());
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsTag("Movement")) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (reticule.transform.position);
			screenPos.Scale (new Vector3(1, 1, 0));
			Quaternion target = Quaternion.Euler (60, 0, 0) * Quaternion.FromToRotation (Vector3.down, Input.mousePosition - screenPos);
			reticule.transform.rotation = Quaternion.RotateTowards(reticule.transform.rotation, target, rotationSpeed);
		}
	}
}
