using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

	private Transform tx;

	// Use this for initialization
	void Start () {
		tx = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.R)) {
			tx.Rotate(new Vector3(0,180,0));
		}
	}

	void rotateCamera(Vector3 rot) {
	}
}
