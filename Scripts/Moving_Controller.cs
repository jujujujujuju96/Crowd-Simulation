using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Controller : MonoBehaviour {
    public float Speed = 3.0f;
	public float lifeTime = 15.0f;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifeTime);
		Speed = Random.Range (2.0f, 3.5f);

		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);


	}
}
