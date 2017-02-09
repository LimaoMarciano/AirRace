using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {

	[Header("Speed")]
	public float maxSpeed = 150.0f;
	public float enginePower = 10.0f;
	public float weight = 1.0f;

	[Header("Manuverability")]
	public float horizontalTurnSpeed = 50.0f;
	public float verticalTurnSpeed = 50.0f;
	public float rollStabilizationSpeed = 100.0f;
	public float dragC = 0.5f;
	public float stallSpeed = 30.0f;

	private Rigidbody rb;
	private float horizontalInput;
	private float verticalInput;

	public float hRotationInput { get; private set; }
	public float vRotationInput { get; private set; }
	private float hRotationV = 0;
	private float vRotationV = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.mass = weight;
	}
	
	// Update is called once per frame
	void Update () {

		horizontalInput = Input.GetAxis ("Horizontal");
		verticalInput = Input.GetAxis ("Vertical");

	}

	void FixedUpdate () {



		//Horizontal rotation
		hRotationInput = Mathf.SmoothDamp(hRotationInput, horizontalInput, ref hRotationV, 0.2f);
		Vector3 hRotationAxis = Vector3.up;
		float hRotationAngle = horizontalTurnSpeed * hRotationInput * Time.fixedDeltaTime;
		Quaternion hRotation = Quaternion.AngleAxis (hRotationAngle, hRotationAxis);

		//Vertical rotation
		vRotationInput = Mathf.SmoothDamp(vRotationInput, verticalInput, ref vRotationV, 0.2f);
		Vector3 vRotationAxis = Vector3.right;
		float vRotationAngle = verticalTurnSpeed * vRotationInput * Time.fixedDeltaTime;
		Quaternion vRotation = Quaternion.AngleAxis (vRotationAngle, vRotationAxis);

		//Automatic Roll adjustment
		Vector3 rollAxis = Vector3.forward;
		float zRotationAngle = rollStabilizationSpeed * Vector3.Dot (Vector3.down, transform.right) * Time.fixedDeltaTime;
		Quaternion zRotation = Quaternion.AngleAxis (zRotationAngle, rollAxis);

		//Apply all rotations
		rb.MoveRotation (rb.rotation * hRotation * vRotation * zRotation);

		//Stabilization
		Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
		Debug.Log (localVelocity);
		float lateralDrag = localVelocity.x * (1 - Time.fixedDeltaTime * dragC);
		float upwardDrag = localVelocity.y * (1 - Time.fixedDeltaTime * dragC);
		Vector3 velocityDamp = new Vector3 (lateralDrag, upwardDrag, localVelocity.z);
		Debug.Log (velocityDamp);
		rb.velocity = transform.TransformDirection (velocityDamp);

		//Acceleration
		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddForce (enginePower * transform.forward);
		}

		//Gravity
		float inclination = Mathf.Abs (Vector3.Dot (Vector3.up, transform.forward));
//		rb.AddForce (10 * Mathf.Abs (gravity) * Vector3.down, ForceMode.Acceleration);

		float lift = Mathf.Clamp01 (Vector3.Dot (rb.velocity, transform.forward));

		float stall = 1 - Mathf.Clamp01 (localVelocity.z / stallSpeed);
//		Debug.Log (stall);

		float gravityInfluence = Mathf.Clamp01 (inclination + lift + stall);

//		rb.AddForce (10 * gravityInfluence * Vector3.down, ForceMode.Acceleration);

	}
}
