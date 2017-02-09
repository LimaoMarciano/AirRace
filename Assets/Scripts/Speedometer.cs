using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

	private Text speedometer;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		speedometer = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		float speed = rb.velocity.magnitude * 3.6f;
		speedometer.text = speed.ToString ("F0");
	}
}
