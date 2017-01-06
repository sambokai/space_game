using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PlayerShipMovement : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed = 0, accl = 15, decl = 15, rollspeed = 5, max_rollspd = 100;
	private int throttle;
	public float pitch_sensitivity = 70;
	public float yaw_sensitivity = 70;

	private Vector3 mousePercFromCenter;
	private Vector3 windowCenter;

	Vector3 angVel;

	// Use this for initialization
	void Start() {
		rigidBody = GetComponent<Rigidbody>();
		//Screen.lockCursor = true;
		Cursor.visible = false;
		MeshRenderer mesh = GetComponent<MeshRenderer>();
		resetMovement ();
	}
	
	// Update is called once per frame
	void Update () {
		updateMousePercFromCenter ();
	}
		
	void FixedUpdate() {

		if (mouseOnScreen()) {
			//pitch
			angVel.x = pitch_sensitivity * mousePercFromCenter.y;
			//yaw
			angVel.y = yaw_sensitivity * mousePercFromCenter.x;
		} else {
			angVel.x = 0;	
			angVel.y = 0;
		}


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
			resetMovement ();
		}



		rigidBody.velocity = (transform.forward * -speed) * Time.fixedDeltaTime;
	}

	void updateMousePercFromCenter()
	{
		updateWindowCenter();
		mousePercFromCenter = Input.mousePosition;
		mousePercFromCenter.x = mousePercFromCenter.x / windowCenter.x - 1;
		mousePercFromCenter.y = mousePercFromCenter.y / windowCenter.y - 1;
	}

	bool mouseOnScreen()
	{
		return mousePercFromCenter.y >= -1 && mousePercFromCenter.y <= 1 && mousePercFromCenter.x >= -1 && mousePercFromCenter.x <= 1;
	}

	void updateWindowCenter()
	{
		windowCenter.Set (Screen.width / 2, Screen.height / 2, 0);
	}

	void resetMovement()
	{
		speed = 0;
		angVel.Set (0, 0, 0);
	}
}
