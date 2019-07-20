using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock_Y_position : MonoBehaviour {
    public float Y_lock;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position_y_locked = transform.position;
		position_y_locked.y = Y_lock;
		transform.position = position_y_locked; 
		
	}
}
