using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMove : MonoBehaviour {
	public GameObject Shujin;//主角
	//public float speed=1f;//移动的阻尼，值越小，移动越平缓
	public float maxdistance = 1.0f;
	public float mindistance = 0.5f;
	public float Force;
    public float distance_Leader_between_Member;

    public Vector3 To_Target;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(1,1,1);
	}

	// Update is called once per frame
	void Update () {
		distance_Leader_between_Member = Vector3.Distance(Shujin.transform.position,transform.position);
		if(distance_Leader_between_Member > maxdistance ){

			//Debug.Log (To_Target);
			PetSmothFlow ();
            //Debug.Log("Flow");
		}
        if (distance_Leader_between_Member < mindistance) {
            PetSeperate();
        }
	}
	void PetSmothFlow(){
		Vector3 To_Target = new Vector3(Shujin.transform.position.x-transform.position.x,
			Shujin.transform.position.y-transform.position.y,
			Shujin.transform.position.z-transform.position.z);
		GetComponent<Rigidbody>().AddForce(To_Target * Force,ForceMode.Impulse);
	}
    void PetSeperate() {
        Vector3 AwayFromTarget = new Vector3(Shujin.transform.position.x - transform.position.x,
            Shujin.transform.position.y - transform.position.y,
            Shujin.transform.position.z - transform.position.z);
        GetComponent<Rigidbody>().AddForce(AwayFromTarget*(-1.0f) * Force, ForceMode.Impulse);

    }
}