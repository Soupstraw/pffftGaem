using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HitpointBar : MonoBehaviour {

	private Slider slider;

	public Damagable damagable;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		slider.maxValue = 100f;
		slider.minValue = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = damagable.health;
	}
}
