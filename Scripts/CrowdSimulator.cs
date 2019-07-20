using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum HumanType {
    idle, active

}

public class CrowdSimulator : MonoBehaviour {
    public HumanType humanType = HumanType.idle;

    [Header("FuneBoko")]
    GameObject funeboko;
    //float dist_fu_stage_1;
    //float dist_fu_stage_2;
    //float dist_fu_stage_3;
    [Header("状態判断変数")]
    public float DistanceIdle;
    public float DistanceActive;
    float DistanceGatherMin,DistanceGatherMax;//凝集判断条件
    private int HumanTypeInitial;
    private bool GatherCondition = false;
    [Header("距離")]
    float MaxDistance;//最大移動距離
    float distance;
    private Vector3 PositionInitial;
    private float DistanceFromStart;
    [Header("動画")]
    public float LookProbility_;
    static float t = 0.0f;
    private Animator animator_;
    private Vector3 pos_;
    private float LookProbility=0.9f;
    [Header("凝集")]
    public float GatherForce;
    public float speed;
    Collider CarCollider;
    [Header("分離")]
    GameObject[] obj;
    public float SeperateForce=100.0f;
    [Header("センサー")]
    float sensorRadius = 0.30f;
   

    // Use this for initialization
    void Start() {
        obj = GameObject.FindGameObjectsWithTag("Crowd");
        PositionInitial = transform.position;
        animator_ = GetComponent<Animator>();
        funeboko = GameObject.Find("Car");
        CarCollider = funeboko.GetComponent<Collider>();
        LookProbility_ = SetValueWithin(0f, 1f);
        MaxDistance = SetValueWithin(0.3f,1.3f);
        DistanceGatherMin = SetValueWithin(2.0f, 3.5f);
        DistanceGatherMax = SetValueWithin(4.0f, 5.5f);
        GetComponent<CapsuleCollider>().radius = sensorRadius*2.0f;
    }
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetInteger("state", 0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Animator>().SetInteger("state",1 );
        }
        distance= Vector3.Distance(funeboko.transform.position, transform.position);
        DistanceFromStart = Vector3.Distance(PositionInitial, transform.position);
        //Debug.Log("<color=#800000ff>"+dist_fu+"</color>"+name);
        switch (humanType) {
            case HumanType.idle:
                updateHumanTypeIdle();
                break;
            case HumanType.active:
                //if (!GatherCondition){
                    updateHumanTypeActive();
                    //GatherCondition = true;
                //}
                break;
        }
      //  if (GatherCondition)
      //  {
       //     GatherConditionReset();
       // }
	}

    void updateHumanTypeIdle() {
        if (distance >= DistanceActive) {
            
            //Animation Idle Here
        }
    }
    void updateHumanTypeActive() {
        Gathering();
        //if (distance <= DistanceIdle) {
            //Vector3 ToFuneboko = (funeboko.transform.position - this.transform.position).normalized;
            //this.transform.Translate(ToFuneboko * MaxDistance);
        //}
    }
    void OnAnimatorIK(int layerIndex) {

        if (LookProbility_ < LookProbility) {

            if (animator_) {
                Vector3 pos = funeboko.transform.position;
                pos_ = Vector3.Lerp(pos_, pos, 0.075f);
                animator_.SetLookAtPosition(pos_);
                float weight_ = Mathf.Lerp(0f, 0.8f, t);
                t += 0.05f * Time.deltaTime;
                animator_.SetLookAtWeight(weight_, 1.0f, 0.7f, 1f, 0.6f);
            }
        }
    }
    int HumanTypeInitialize() {
        float i = Random.Range(0, 1);
        if (i <= 0.7)
            HumanTypeInitial = 0;
        else {
            HumanTypeInitial = 1; 
        }
        return HumanTypeInitial;
    }
    void Gathering() {
        //Debug.Log(distance);//ランダム待ち時間
        if (distance <= DistanceGatherMax && distance >= DistanceGatherMin){
            if (DistanceFromStart <= MaxDistance)
            {
                Debug.Log("Gather");
                Vector3 ToFuneboko = new Vector3(funeboko.transform.position.x - this.transform.position.x, 0, funeboko.transform.position.z - this.transform.position.z);
                //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0.5f);
                GetComponent<Rigidbody>().AddForce(ToFuneboko * GatherForce, ForceMode.Impulse);
                //transform.DOMove(ToFuneboko, speed);
                //GatherCondition = true;
            }
        }
        //if (distance < 3.0f) {
          //  Debug.Log("Away From");
           // Vector3 ToFuneboko = new Vector3(funeboko.transform.position.x - this.transform.position.x, 0, funeboko.transform.position.z - this.transform.position.z);
           // GetComponent<Rigidbody>().AddForce(ToFuneboko * GatherForce*(-2.0f), ForceMode.Impulse);
       // }
    }
    /// <summary>
    /// 没用了，触发不了
    /// </summary>
    /// <param name="e"></param>
    void OnTriggerEnter(Collider e) {
        if (e.gameObject.tag.CompareTo("Car") == 0) {
            Debug.Log("Car");
            GetComponent<Rigidbody>().Sleep();
        }
        
    }
    void OnTriggerStay(Collider e) {
        if (e.gameObject.tag.CompareTo("Crowd") == 0)
        {
            Debug.Log("separete");
            Vector3 seperateDirection = new Vector3(this.transform.position.x - e.gameObject.transform.position.x, 0, this.transform.position.z - e.gameObject.transform.position.z);
            seperateDirection = seperateDirection.normalized;
            Debug.Log(seperateDirection);
            GetComponent<Rigidbody>().AddForce(seperateDirection * SeperateForce, ForceMode.Impulse);

        }
    
    }
    //IEnumerator GatherConditionReset() {
    //    yield return new WaitForSeconds(0.5f);
   //     GatherCondition = false;
   // }
    float SetValueWithin(float min,float max) {
        float temp = Random.Range(min,max);
        return temp;
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }
}
