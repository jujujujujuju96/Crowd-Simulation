using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovingController : MonoBehaviour {
    public float times;
	public float Wait_Time = 20.0f;
	public float Time_CountDown = 0;
    public GameObject Moving_People_right;
    GameObject People_temp;


    // Use this for initialization
    void Start() {
		//StartCoroutine (PeopleCreation ());
		//InvokeRepeating("PeopleCreation", times, 3);
    }

    // Update is called once per frame
    void Update() {
		Time_CountDown -= Time.deltaTime;
		if (Time_CountDown <= 0) {
			Time_CountDown = Random.Range (3.0f, 5.0f);

			for (int i = 0;i < Random.Range(1, 3); i++)
			{
				Vector3 CreatPosition = new Vector3(-15.0f, 0.5f, Random.Range(-8, -11));
				Quaternion CreatRotation = Quaternion.Euler(1, 0, 0);
				People_temp = Instantiate(Moving_People_right,CreatPosition,CreatRotation);

			}
		
		}
		//times = Random.Range(0.3f, 1.5f);

    }

	/*void  PeopleCreation() {
		//yield return new WaitForSeconds (Wait_Time);
        for (int i = 0;i < Random.Range(1, 3); i++)
        {
            Vector3 CreatPosition = new Vector3(-15.0f, 0.5f, Random.Range(-8, -11));
            Quaternion CreatRotation = Quaternion.Euler(1, 0, 0);
            People_temp = Instantiate(Moving_People_right,CreatPosition,CreatRotation);

        }
		times = Random.Range(0.3f, 15f);
        
        

    }*/
}
