using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityController : MonoBehaviour {
	float vy = 0.0f;
	// Use this for initialization
	void Start () {
		

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 v = GetComponent<Rigidbody> ().velocity;
		v.y = vy;
		GetComponent<Rigidbody> ().velocity = v;

		
	}
}
