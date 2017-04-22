using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttacker : MonoBehaviour {
	public float timeout;
	public float speed;
	Vector2

	void Start () {
		 = transform.eulerAngles.z / 180 * Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
