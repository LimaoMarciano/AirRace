using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {

	public float maxSpeed = 150.0f;
	public float acceleration = 50.0f;

	public float horizontalTurnSpeed = 50.0f;
	public float verticalTurnSpeed = 50.0f;

	private Rigidbody rb;
	private float horizontalInput;
	private float verticalInput;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");

	}

	void FixedUpdate () {

		//Horizontal rotation
		Vector3 hRotationAxis = Vector3.up;
		float hRotationAngle = horizontalTurnSpeed * horizontalInput * Time.fixedDeltaTime;
		Quaternion hRotation = Quaternion.AngleAxis (hRotationAngle, hRotationAxis);
		rb.MoveRotation (rb.rotation * hRotation);

		//Vertical rotation
		Vector3 vRotationAxis = Vector3.right;
		float vRotationAngle = verticalTurnSpeed * verticalInput * Time.fixedDeltaTime;
		Quaternion vRotation = Quaternion.AngleAxis (vRotationAngle, vRotationAxis);
		rb.MoveRotation (rb.rotation * vRotation);

		Vector3 zRotationAxis = Vector3.forward;
		float zRotationAngle = 100 * Vector3.Dot (Vector3.down, transform.right) * Time.fixedDeltaTime;
		Quaternion zRotation = Quaternion.AngleAxis (zRotationAngle, zRotationAxis);
		rb.MoveRotation (rb.rotation * zRotation);

		if (rb.velocity.magnitude < maxSpeed) {

			rb.AddForce (acceleration * transform.forward, ForceMode.Acceleration);

		}

		Vector3 cross = Vector3.Cross (Vector3.down, transform.up);

		Debug.Log (Vector3.Dot (Vector3.down, transform.right));

//		Debug.DrawLine (transform.position, transform.position + cross.normalized * 5, Color.red);

	}
}
