using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	public Camera camera;

	public float amplitude = 0.01f;
	public float freqMultiplier = 0.5f;
	public float decay = 1.0f;

	private float intensity = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		camera.transform.position = transform.position + new Vector3 (amplitude * intensity * Mathf.Sin(intensity*freqMultiplier), amplitude * intensity / 2 * Mathf.Sin(intensity/3*freqMultiplier), -10);
		intensity = Mathf.Clamp (intensity - Time.deltaTime * decay, 0, float.PositiveInfinity);
	}

	public void ApplyShake(float intensity){
		this.intensity += intensity;
	}
}
