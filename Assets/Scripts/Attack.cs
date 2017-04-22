using System.Collections;
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
			if(OnLightAttack != null)
				OnLightAttack (lightAttack.GetComponent<Collider2D>());
		}
		if (Input.GetButtonDown ("HeavyAttack")) {
			anim.SetTrigger ("HeavyAttack");
			if(OnHeavyAttack != null)
				OnHeavyAttack (heavyAttack.GetComponent<Collider2D>());
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsTag("Movement")) {
			Quaternion target = Quaternion.Euler (60, 0, 0) * Quaternion.FromToRotation (Vector3.down, Input.mousePosition - Camera.main.WorldToScreenPoint(reticule.transform.position));
			reticule.transform.rotation = Quaternion.RotateTowards(reticule.transform.rotation, target, rotationSpeed);
		}
	}
}
