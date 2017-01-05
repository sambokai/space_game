using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PlayerShipMovement : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed = 0, accl = 15, decl = 15, rollspeed = 5, max_rollspd = 100, yawspeed = 5;
	public int sensitivity = 100;

	Vector3 angVel;

	// Use this for initialization
	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		Screen.lockCursor = true;
		MeshRenderer mesh = GetComponent<MeshRenderer>();
		Debug.Log (mesh.bounds.size.x);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {

		//pitch
		angVel.x += -Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

		//yaw

		angVel.y += Input.GetAxis ("Mouse X") * yawspeed * Time.fixedDeltaTime;

//		if (Input.GetKey (KeyCode.D)) {
//			
//			angVel.y += yawspeed;
//		} else if (Input.GetKey (KeyCode.A)) {
//			
//			angVel.y -= yawspeed;
//		}
//
		//roll
		if (Input.GetKey (KeyCode.E)) {

			if (Mathf.Abs(angVel.z) <= max_rollspd) {
				angVel.z += rollspeed;
			}
		} else if (Input.GetKey (KeyCode.Q)) {

			if (Mathf.Abs(angVel.z) <= max_rollspd) {
				angVel.z -= rollspeed;
			}
		}

		else if (angVel.z > 0) {
			angVel.z -= 5;
		} else if (angVel.z < 0) {
			angVel.z += 5;
		}


		//angular velocity limit
		// angVel -= angVel.normalized * angVel.sqrMagnitude * .08f * Time.fixedDeltaTime;

		//Rotate
		Quaternion deltaRotation = Quaternion.Euler (angVel * Time.fixedDeltaTime);
		rigidBody.MoveRotation (rigidBody.rotation * deltaRotation);

		// Acceleration
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed += accl * Time.fixedDeltaTime;
		} else if (Input.GetKey (KeyCode.LeftControl)) {
			speed -= decl * Time.fixedDeltaTime;
		} else if (Input.GetKey (KeyCode.Space)) {
			speed = 0;
			angVel.Set (0, 0, 0);
		}



		rigidBody.velocity = (transform.forward * -speed) * Time.fixedDeltaTime;
	}
}
