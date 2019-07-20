using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationController : MonoBehaviour {
	public float index = 0.0f;
	private int keey = 1;
	// Use this for initialization
	void Start () {
		index = Random.Range (0, 100);
		//Debug.Log (index);
		if (index <= 15 && keey == 1) {

			//Debug.Log ("Red");
			GetComponent<MeshRenderer> ().material.color = Color.green;
			keey = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {


		
	}
}
