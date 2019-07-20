using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunebokoController : MonoBehaviour {
	public float Speed = 4.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Speed * Time.deltaTime);
		
	}
}
