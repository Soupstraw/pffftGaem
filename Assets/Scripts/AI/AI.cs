using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	GameObject player;
	Rigidbody2D rb;
	Damagable damagable;
	Animator anim;

	public float speed = 1.0f;
	public GameObject firstAttack;
	public GameObject secondAttack;

	void Start () {
		player = GameObject.FindWithTag("Player");
		rb = gameObject.GetComponent<Rigidbody2D> ();
		damagable = GetComponent<Damagable> ();
		anim = GetComponent<Animator> ();
	}

	public void Stop() {
		rb.velocity = Vector2.zero;
	}

	public void Beeline() {
		Vector3 direction = player.transform.position - transform.position;
		if (Vector3.Magnitude (direction) > 0.3) {
			rb.velocity = new Vector2 (direction.x, direction.y).normalized * speed;
		} else {
			rb.velocity = Vector2.zero;
		}
	}

	public void KeepDistance(float distance) {
		Vector3 reverseDirection = (transform.position - player.transform.position);
		if (Vector3.Magnitude (reverseDirection) > 0.3) {
			reverseDirection.z = 0;
			reverseDirection = reverseDirection.normalized * distance + player.transform.position - transform.position;
			rb.velocity = new Vector2 (reverseDirection.x, reverseDirection.y).normalized * speed;
		} else {
			rb.velocity = Vector2.zero;
		}
	}

	public void BasicAttack(GameObject projectile) {
		Vector3 direction = (player.transform.position - transform.position).normalized;
		Quaternion rotation = Quaternion.identity;
		float rot = Mathf.Atan(direction.y / direction.x) / Mathf.PI * 180;
		if (direction.x < 0) {
			rot += 180;
		}
		rotation.eulerAngles = new Vector3 (0, 0, rot);
		Instantiate (projectile, transform.position, rotation);
		anim.SetTrigger ("Attack");
	}

	public IEnumerator Evade(Vector3 direction, float speed, float time) {
		float startTime = Time.time;
		while (Time.time - startTime < time) {
			damagable.enabled = false;
			rb.velocity = direction.normalized * (speed * 2 * (1 - (Time.time - startTime) / time));
			yield return null;
		}
		damagable.enabled = true;
	}

	void Update(){
		if (anim != null) {
			Vector3 moveDir = rb.velocity.normalized;
			anim.SetFloat ("MoveX", moveDir.x);
			anim.SetFloat ("MoveY", moveDir.y);
		}
	}

}
