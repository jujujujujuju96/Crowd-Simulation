using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolding : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
}
