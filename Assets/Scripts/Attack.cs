using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Attack : MonoBehaviour {

	private Animator anim;

	public float rotationSpeed = 10f;
	private bool updateRotation = true;

	public Attacker lightAttack;
	public Attacker heavyAttack;
	public Attacker slamAttack;
	public Attacker spinAttack;
	public Attacker turnAttack;

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

	public void TurnAttack(){
		turnAttack.DealDamage ();
	}

	public void LightWarn(){
		lightAttack.WarnAttack ();
	}

	public void HeavyWarn(){
		heavyAttack.WarnAttack ();
	}

	public void SlamWarn(){
		slamAttack.WarnAttack ();
	}

	public void SpinWarn(){
		spinAttack.WarnAttack ();
	}

	public void TurnWarn(){
		turnAttack.WarnAttack ();
	}

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void Charge(){
		GetComponentInParent<Charge> ().DoCharge (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * Vector3.down);
	}

	public void Spin(float speed){
		Debug.Log ("Spinning " + speed);
		StartCoroutine (SpinCoroutine (speed));
	}

	IEnumerator SpinCoroutine(float speed){
		Quaternion target = Quaternion.Euler(60, 0, transform.rotation.eulerAngles.z - 179f);
		while (Quaternion.Angle(transform.rotation, target) > 0.1f) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speed * Time.deltaTime);
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("LightAttack")) {
			anim.SetTrigger ("LightAttack");
		}
		if (Input.GetButtonDown ("HeavyAttack")) {
			anim.SetTrigger ("HeavyAttack");
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName("Idle") && updateRotation) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			screenPos.Scale (new Vector3(1, 1, 0));
			Quaternion target = Quaternion.Euler (60, 0, 0) * Quaternion.FromToRotation (Vector3.down, Input.mousePosition - screenPos);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
		}
	}
}
