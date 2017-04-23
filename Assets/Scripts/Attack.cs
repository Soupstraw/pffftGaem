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

	public Attacker lightAttack;
	public Attacker heavyAttack;
	public Attacker slamAttack;
	public Attacker spinAttack;

	public void LightAttack(){
		lightAttack.DealDamage ();
	}

	public void HeavyAttack(){
		heavyAttack.DealDamage ();
	}

	public void SlamAttack(){
		slamAttack.DealDamage ();
	}

	public void SpinAttack(){
		spinAttack.DealDamage ();
	}

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void Charge(){
		GetComponentInParent<Charge> ().DoCharge (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * Vector3.down);
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
		if (anim.GetCurrentAnimatorStateInfo (0).IsName("Idle")) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			screenPos.Scale (new Vector3(1, 1, 0));
			Quaternion target = Quaternion.Euler (60, 0, 0) * Quaternion.FromToRotation (Vector3.down, Input.mousePosition - screenPos);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed);
		}
	}
}
