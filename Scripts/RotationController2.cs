using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController2 : MonoBehaviour
{

    private float speed = 50.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
