using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.None;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition) + new Vector3(0, 0, 1);
		transform.rotation = Quaternion.FromToRotation (Vector3.up, Input.mousePosition - Camera.main.WorldToScreenPoint(GameObject.FindWithTag("Player").transform.position));
	}
}
