﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : MonoBehaviour {

	public delegate void AttackAction(Attacker attacker);
	public static event AttackAction OnAttack;

	public int ticks = 1;
	public float damage;
	public float knockback = 0f;
	public LayerMask receiver;

	public bool sidewaysKnockback = false;

	private Collider2D coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<Collider2D>();
	}

	public void WarnAttack(){
		if (OnAttack != null) {
			OnAttack (this);
		}
		if (gameObject.name == "HeavyAttack" || gameObject.name == "LightAttack") {
			ContactFilter2D filter = new ContactFilter2D ();
			filter.SetLayerMask (receiver);
			Collider2D[] res = new Collider2D[100];
			Physics2D.OverlapCollider (coll, filter, res);
			foreach(Collider2D col in res){
				if (col != null) {
					EvasiveAI ai = col.GetComponent<EvasiveAI> ();
					if (ai != null)
						ai.TryEvade (gameObject);
				}
			}
		}
	}

	public void DealDamage(){
		ContactFilter2D filter = new ContactFilter2D ();
		filter.SetLayerMask (receiver);
		Collider2D[] res = new Collider2D[100];
		Physics2D.OverlapCollider (coll, filter, res);
		foreach(Collider2D col in res){
			if (col != null) {
				Damagable dam = col.GetComponent<Damagable> ();
				if (dam != null) {
					dam.DealDamageOverTime (damage, ticks);
					if (sidewaysKnockback) {
						dam.Knockback (transform.rotation * Vector3.right);
					} else {
						dam.Knockback ((dam.transform.position - coll.transform.position).normalized * knockback);
					}
					FindObjectOfType<ScreenShake> ().ApplyShake (10f);
					SpriteRenderer sprite = col.GetComponent<SpriteRenderer> ();
					if(sprite != null && damage > 0 && dam.GetComponent<Animator>() == null){
						StartCoroutine (HitGlow(10, sprite));
					}
				}
			}
		}
	}

	IEnumerator HitGlow(float damage, SpriteRenderer sprite){
		float startTime = Time.time;
		float duration = damage * 0.05f;

		while (Time.time - startTime < duration && sprite != null) {
			sprite.color = Color.Lerp (Color.red, Color.white, (Time.time - startTime) / duration);
			yield return null;
		}

		if (sprite != null) {
			sprite.color = Color.white;
		}
	}
}
