using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PlayerShipMovement : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed = 0, accl = 15, decl = 15, rollspeed = 5;
	public int sensitivity = 100;

	Vector3 angVel;

	// Use this for initialization
	void Start() {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate() {

		//pitch
		angVel.x += -Input.GetAxis("Vertical") * Mathf.Abs(-Input.GetAxis("Vertical")) * sensitivity * Time.fixedDeltaTime;

		//roll and yaw
		if (Input.GetKey (KeyCode.E)) {

			angVel.z += rollspeed;
		} else if (Input.GetKey (KeyCode.Q)) {

			angVel.z -= rollspeed;
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
		}



		rigidBody.velocity = (transform.forward * -speed) * Time.fixedDeltaTime;
	}
}
