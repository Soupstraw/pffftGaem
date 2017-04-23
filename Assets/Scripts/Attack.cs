using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Attack : MonoBehaviour {

	public float angle = 1f;

	public Vector3 attackDir = new Vector3();

	private Animator anim;

	public float rotationSpeed = 10f;
	private bool updateRotation = true;

	public float triggerTime = 0.3f;

	public Attacker lightAttack;
	public Attacker heavyAttack;
	public Attacker slamAttack;
	public Attacker spinAttack;
	public Attacker turnAttack;

	public ParticleSystem slamParticles;

	public float movementMultiplier = 1.0f;
	public bool charging = false;
	public bool canAttack = true;

	public void ScreenShake(float intensity){
		FindObjectOfType<ScreenShake>().ApplyShake (intensity);
	}

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

	public void BashAnimation(){
		GameObject.FindWithTag("Player").GetComponent<Animator> ().SetTrigger("Bash");
	}

	void Start(){
		anim = GetComponent<Animator> ();
	}

	public void Charge(){
		GetComponentInParent<Charge> ().DoCharge (Quaternion.Euler (0, 0, transform.rotation.eulerAngles.z) * Vector3.down);
	}

	public void Spin(float speed){
		StartCoroutine (SpinCoroutine (speed));
	}

	public void SlamParticles(){
		slamParticles.Play ();
	}

	IEnumerator SpinCoroutine(float speed){
		Quaternion target = Quaternion.Euler(angle, 0, transform.rotation.eulerAngles.z - 179f);
		while (Quaternion.Angle(transform.rotation, target) > 0.1f) {
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speed * Time.deltaTime);
			yield return null;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("LightAttack") && canAttack) {
			anim.SetBool ("LightAttack", true);
		}
		if (Input.GetButtonDown ("HeavyAttack") && canAttack) {
			anim.SetBool ("HeavyAttack", true);
		}
		if (!canAttack) {
			anim.SetBool ("HeavyAttack", false);
			anim.SetBool ("LightAttack", false);
		}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle") && updateRotation) {
			Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);
			screenPos.Scale (new Vector3 (1, 1, 0));
			Quaternion target = Quaternion.Euler (angle, 0, 0) * Quaternion.FromToRotation (Vector3.down, Input.mousePosition - screenPos);
			//Quaternion target = Quaternion.Euler (angle, 0, 0) * Quaternion.FromToRotation (Vector3.down, GetComponentInParent<Movement>().lastNotZero);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, target, rotationSpeed * Time.deltaTime);
			//attackDir = Vector3.zero;
		}
		attackDir = transform.rotation * Vector3.down;
	}
}
