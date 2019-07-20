using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMoving2 : MonoBehaviour {
    float times;
    int i = 0;
    int m = 0;
    //public float j = 0.0f;
    float Time_CountDown = 0;
    Vector3 CreatPosition;

    [Header("生成tample")]
    public GameObject[] SamplePrefabs;
    //public GameObject SamplePrefabs;

    [Header("生成時間の最小限と最大限")]
    public float TimeMin, TimeMax;

    [Header("生成角度")]
    public Quaternion CreatRotation;

    [Header("父対象")]
    public Transform fatherObject;

    GameObject People_temp1;
    [Header("環境設定")]
    public GameObject envDanger;
    public GameObject envInterest_2;
    public GameObject Perceiver;
    //GameObject envDanger;
    //GameObject envInterest_2;
   // GameObject Perceiver;

    //GameObject People_temp2;
    //public GameObject SampleObject;



    // Use this for initialization
    void Start() {
       // Instantiate(SamplePrefabs, CreatPosition, CreatRotation);


        //People_temp.GetComponent<MeshRenderer> ().material.color = Color.blue;
        //InvokeRepeating("PeopleCreation", times, 3);
    }

    // Update is called once per frame
    void Update() {
        Time_CountDown -= Time.deltaTime;
        if (Time_CountDown <= 0) {

            SingleCreation();
            Time_CountDown = Random.Range(TimeMin, TimeMax);

            //j = Random.Range (0.0f,	1.0f);
            /*for (int i = 0;i < Random.Range(1, 1); i++)
			{
				Vector3 CreatPosition = new Vector3(20.0f, 0.5f, Random.Range(8, 14));
				Quaternion CreatRotation = Quaternion.Euler(1, 0, 0);
				People_temp = Instantiate(Moving_People_left,CreatPosition,CreatRotation);

			}*/
            //if (j >= 0.3) {
            //} else {
            //	CoupleCreation ();
            //}

        }

    }
    /*void PeopleCreation() {
		for (int i = 0;i < Random.Range(1, 3); i++)
		{
			Vector3 CreatPosition = new Vector3(15.0f, 0.5f, Random.Range(8, 11));
			Quaternion CreatRotation = Quaternion.Euler(1, 0, 0);
			People_temp = Instantiate(Moving_People_left,CreatPosition,CreatRotation);

		}
		times = Random.Range(0.3f, 1.5f);


	}*/

    void SingleCreation() {
        int randomIndex = Random.Range(0, SamplePrefabs.Length);
         Debug.Log(randomIndex);
         Debug.Log(SamplePrefabs.Length);
        CreatPosition = SamplePrefabs[randomIndex].transform.position;
        //CreatPosition = new Vector3(44.0f, 0.0f, 2.17f);
        //CreatPosition.x = -22.0f;
        //CreatPosition.z = 2.17f;
        //CreatPosition.z = Random.Range(CreatPosition.z + 0.3f, CreatPosition.z - 0.1f); ;
        Quaternion CreatRotation = Quaternion.Euler(-1, 0, 0);
        People_temp1 = Instantiate(SamplePrefabs[randomIndex], CreatPosition, CreatRotation);
       // CreatPosition = SamplePrefabs.transform.position;
       // CreatPosition.z = Random.Range(CreatPosition.z + 0.3f, CreatPosition.z - 0.1f); ;
       // Quaternion CreatRotation = Quaternion.Euler(1, 0, 0);
       // People_temp1 = Instantiate(SamplePrefabs, CreatPosition, CreatRotation);
        i++;
        People_temp1.tag = "clone";
        People_temp1.name = "Single" + i;
        People_temp1.transform.parent = fatherObject;
    }
    /***void CoupleCreation(){
		float z = Random.Range (SampleObject.transform.position.z + 0.5f, SampleObject.transform.position.z - 0.5f);
        CreatPosition.z = z;
		Quaternion CreatRotation = Quaternion.Euler(1, 0, 0);
		People_temp1 = Instantiate(Leader_Sample,CreatPosition,CreatRotation);
		m++;
		People_temp1.name = "Couple_Leader"+ m;

		z = z + Random.Range (-1, 1);
		Vector3 CreatPosition2 = new Vector3(25.0f, 0.5f, z);
		Quaternion CreatRotation2 = Quaternion.Euler(1, 0, 0);
		People_temp2 = Instantiate(Member_Sample,CreatPosition2,CreatRotation2);
		People_temp2.name = "Couple_Member"+ m;
		People_temp2.GetComponent<PetMove> ().Shujin = People_temp1; 
	}***/
}