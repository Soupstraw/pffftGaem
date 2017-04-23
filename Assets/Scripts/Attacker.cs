using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attacker : MonoBehaviour {

	public delegate void AttackAction(Attacker attacker);
	public event AttackAction OnAttack;

	public int ticks = 1;
	public float damage;
	public float knockback = 0f;
	public LayerMask receiver;

	private Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
	}

	public void WarnAttack(){
		if (OnAttack != null) {
			OnAttack (this);
		}
	}

	public void DealDamage(){
		ContactFilter2D filter = new ContactFilter2D ();
		filter.SetLayerMask (receiver);
		Collider2D[] res = new Collider2D[100];
		Physics2D.OverlapCollider (collider, filter, res);
		foreach(Collider2D col in res){
			if (col != null) {
				Damagable dam = col.GetComponent<Damagable> ();
				if (dam != null) {
					dam.DealDamageOverTime (damage, ticks);
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
