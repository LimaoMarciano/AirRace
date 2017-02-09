using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularSpeedRotation : MonoBehaviour {

	public Airplane airplane;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {

		float horizontalInput = airplane.hRotationInput;
		Quaternion rotation = Quaternion.AngleAxis (-horizontalInput * 60, Vector3.forward);

		transform.localRotation = rotation;

	}
}
