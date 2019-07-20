using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AI : MonoBehaviour
{
    [Header("FuneBoko")]
    GameObject funeboko;
    Vector3 PositonFuneboko;
    [Header("状態判断変数")]
    float DistanceGatherMin, DistanceGatherMax;//凝集判断条件
    public float IdleRate = 0.8f;
    public float _IdleRate;
    public bool IsGetChimaki=false;
    public bool IsChimakiExist = true;
    public bool HasUActive = false;
    public float UActiveable = 0.0f;
    public enum UIdleState { talkRight, talkLeft, call1, call2, photo };
    public UIdleState uIdlestate;
    [Header("距離")]
    float MaxActiveDistance;//最大移動距離
    float distance;
    private Vector3 PositionInitial;
    private float DistanceTravelled;
    Vector3 lastPosition;
    [Header("動画")]
    public float LookProbility_;
    static float t = 0.0f;
    private Animator animator_;
    private Animator anim;
    private Vector3 pos_;
    private float LookProbility = 0.8f;
    AnimatorStateInfo animInfo;
    [Header("凝集")]
    public float GatherForce;
    public float speed;
    Vector3 ToFuneboko;
    Collider CarCollider;
    GameObject[] obj;

    [Header("分離")]
    public  float SeperateForce = 0.1f;
    public  float SeperateRadius;

    [Header("粽相関")]
    GameObject timaki;
    GameObject timakiParent;
    GameObject father;
    Vector3 timakiWorldPos;
    Vector3 ToTimaki;
    float timakiDistance;
    private float TimakiForce = 4.0f;
    [Header("センサー")]
    float sensorRadius = 0.30f;

    public bool switchState = false;
    public float gameTimer;
    public enum HumanState {Idle,uIdle,Active,UActive,UActive2}
    public int seconds = 0;
    public HumanState state;
    public StateMachine<AI> stateMachine { get; set; }

    private void Start()
    {
        obj = GameObject.FindGameObjectsWithTag("Crowd");
        PositionInitial = transform.position;
        animator_ = GetComponent<Animator>();
        funeboko = GameObject.Find("Funehoko");
        CarCollider = funeboko.GetComponent<Collider>();
        MaxActiveDistance = SetValueWithin(2.8f, 3.5f);
        lastPosition = transform.position;
        DistanceGatherMin = SetValueWithin(0.5f, 0.7f);
        DistanceGatherMax = SetValueWithin(7.0f, 7.5f);
        GetComponent<CapsuleCollider>().radius = sensorRadius * 2.0f;

        anim = this.GetComponent<Animator>();

        timaki = GameObject.Find("Timaki");
        timakiParent = timaki.transform.parent.gameObject;
        father = transform.parent.gameObject;
        //seperate Collider Radius
        SeperateRadius = SetValueWithin(0.2f, 0.4f);
        GetComponent<CapsuleCollider>().radius = SeperateRadius;

        stateMachine = new StateMachine<AI>(this);
        _IdleRate = SetValueWithin(0, 1);
        UActiveable = SetValueWithin(0, 1);
        //最初からIdle状態かUIdle状態かを決める、UIdle状態は変えない
        if (state != HumanState.uIdle)
        {
            stateMachine.ChangeState(FirstState.Instance);
        }
        else {
            stateMachine.ChangeState(SecondState.Instance);
        }
        /*if ( _IdleRate <= IdleRate)
        {
            stateMachine.ChangeState(FirstState.Instance);
        }else
        {
            stateMachine.ChangeState(SecondState.Instance);            
        }*/
    }

    private void Update()
    {
        DistanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        PositonFuneboko = new Vector3(funeboko.transform.position.x,0,funeboko.transform.position.y);
        distance= Vector3.Distance(funeboko.transform.position, transform.position);
        ToFuneboko = new Vector3(funeboko.transform.position.x - this.transform.position.x, 0, funeboko.transform.position.z - this.transform.position.z);

        animInfo = anim.GetCurrentAnimatorStateInfo(0); 

        //粽座標
        timakiWorldPos = timakiParent.transform.TransformPoint(timaki.transform.localPosition);
        timakiDistance = Vector3.Distance(timakiWorldPos, transform.position);
        ToTimaki = new Vector3(timakiWorldPos.x - this.transform.position.x, 0, timakiWorldPos.z - this.transform.position.z);

        //非UIdle非Uactiveである場合、回転する
        if (state != HumanState.uIdle && state != HumanState.UActive)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ToFuneboko), Time.deltaTime);
        }
        //Active範囲内かつ最大移動距離内、Active状態に変更
        if ((distance <= DistanceGatherMax && distance >= DistanceGatherMin && state != HumanState.uIdle && DistanceTravelled <= MaxActiveDistance && HasUActive == false)&&transform.position.z>-0.2f)
        {
            Gather(ToFuneboko,GatherForce);
            state = HumanState.Active;
            stateMachine.Update();
        }
        if ((father.tag == "Missing") && distance <= DistanceGatherMax && distance >= DistanceGatherMin && DistanceTravelled <= MaxActiveDistance) {
            state = HumanState.Active;
            stateMachine.Update();
        }
        
        //Active範囲外になったら、Idle状態に戻す
        if ((distance > DistanceGatherMax && state == HumanState.Active) || (DistanceTravelled > MaxActiveDistance && state == HumanState.Active))
        {
            state = HumanState.Idle;
            stateMachine.Update();
        }

        //粽を拾う,into UActive state
        if (timakiWorldPos.y <= 1.50f && state==HumanState.Active && timakiDistance < 4.0f && IsGetChimaki==false && IsChimakiExist == true && father.tag!="Missing" ) {
                Gather(ToTimaki, TimakiForce);
                state = HumanState.UActive;
                stateMachine.Update();
                HasUActive = true;
                //Destroy(timaki);
            
        }
        if (state == HumanState.UActive && timakiDistance <= 0.2f) {
            state = HumanState.UActive2;
            stateMachine.Update();
            IsGetChimaki = true;
        }
        if (father.tag=="Missing")
        {
            //Debug.Log("Missing Timaki");
        }
    }




    //Animation Controller Start
    public void Simulation_Idle(){
        int  a = (int)SetValueWithin(1.0f, 7.0f);
        anim.Play("Idle_Neutral_2", -1, Random.Range(0.0f, 1.0f));
        this.name = "Idle Project"+a;
        switch (a) {
            case 1:
                anim.Play("Idle_Neutral_1", 2, Random.Range(0.0f, 1.0f));
                break;
            case 2:
                //anim.SetBool("ToIdle2", true);
                anim.Play("Idle_Neutral_2", 2, Random.Range(0.0f, 1.0f));
                break;
            case 3:
                anim.Play("Looking_Around_1", 2, Random.Range(0.0f, 1.0f));
                break;
            case 4:
                anim.Play("Looking_Around_2", 2, Random.Range(0.0f, 1.0f));
                break;
            case 5:
                anim.Play("Coughing_Idle", 2, Random.Range(0.0f, 1.0f));
                break;
            case 6:
                anim.Play("Idle_1", 2, Random.Range(0.0f, 1.0f));
                break;
        }
    }
    public void Simulation_uIdle(){
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        int a = (int)SetValueWithin(1.0f, 6.0f);
        this.name = "uIdle Project" + a;
        anim.Play("Idle_Neutral_2", -1, Random.Range(0.0f, 1.0f));
        /*switch (a)
        {
            case 1:
                anim.Play("talking to cell phone", 2, Random.Range(0.0f, 1.0f));
                break;
            case 2:
                anim.Play("uIdle_talk_right", 2, Random.Range(0.0f, 1.0f));
                break;
            case 3:
                anim.Play("uIdle_talk_left", 2, Random.Range(0.0f, 1.0f));
                break;
            case 4:
                anim.Play("uIdle_calling", 2, Random.Range(0.0f, 1.0f));
                break;
            case 5:
                anim.Play("uIdle_photoing", 2, Random.Range(0.0f, 1.0f));
                break;
        }*/
        switch (uIdlestate)
        { 
            case (UIdleState.call1):
                anim.Play("talking to cell phone", 2, Random.Range(0.0f, 1.0f));
                break;
            case (UIdleState.call2):
                anim.Play("uIdle_calling", 2, Random.Range(0.0f, 1.0f));
                break;
            case (UIdleState.photo):
                anim.Play("uIdle_photoing", 2, Random.Range(0.0f, 1.0f));
                break;
            case (UIdleState.talkLeft):
                anim.Play("uIdle_talk_left", 2, Random.Range(0.0f, 1.0f));
                break;
            case (UIdleState.talkRight):
                anim.Play("uIdle_talk_right", 2, Random.Range(0.0f, 1.0f));
                break;
        }
    }
    public void Simulation_Active(){
        int a = (int)SetValueWithin(1.0f, 6.0f);
        this.name = "Active Project" + a;
        anim.Play("SlowWalkFWD", -1, Random.Range(0.0f, 1.0f));
        //Debug.Log("Active layer" + anim.GetLayerIndex("Active Layer"));
        switch (a)
        {
            case 1:
                //anim.SetBool("ToActive1", true);
                anim.Play("Active_1", 2, Random.Range(0.0f, 1.0f));
                break;
            case 2:
                anim.Play("Active_2", 2, Random.Range(0.0f, 1.0f));
                //anim.SetBool("ToActive2", true);
                break;
            case 3:
                anim.Play("Active_3", 2, Random.Range(0.0f, 1.0f));
                //anim.SetBool("ToActive3", true);
                break;
            case 4:
                anim.Play("Active_4", 2, Random.Range(0.0f, 1.0f));
                //anim.SetBool("ToActive4", true);
                break;
            case 5:
                anim.Play("Active_5", 2, Random.Range(0.0f, 1.0f));
                //anim.SetBool("ToActive5", true);
                break;
        }
    }
    public void Simulation_uActive()
    {
        this.name = "uActive Project";
        anim.Play("SlowWalkFWD", -1, Random.Range(0.0f, 0.3f));
        anim.speed = 1.3f;
        anim.Play("WalkFWD", 2, Random.Range(0.0f, 1.0f));
        
    }
    public void Simulation_uActive2(){
        this.name = "uActive2 Project";
        anim.Play("New State", 2, Random.Range(0.0f, 1.0f));
        anim.Play("pickup", -1, 0.0f);
    }


    //
    void Gather(Vector3 vec,float forc)
    {
        
            //Debug.Log("Gather");
            GetComponent<Rigidbody>().AddForce(vec * forc, ForceMode.Force);
    }
    void OnTriggerStay(Collider e)
    {
        if (e.gameObject.tag.CompareTo("Crowd") == 0)
        {
            //Debug.Log("separete");
            Vector3 seperateDirection = new Vector3(this.transform.position.x - e.gameObject.transform.position.x, 0, this.transform.position.z - e.gameObject.transform.position.z);
            seperateDirection = seperateDirection.normalized;
            //Debug.Log(seperateDirection);
            GetComponent<Rigidbody>().AddForce(seperateDirection * SeperateForce, ForceMode.Force);

        }

    }

    float SetValueWithin(float min, float max)
    {
        float temp = Random.Range(min, max);
        return temp;
    }
    void OnAnimatorIK(int layerIndex)
    {
        if (state != HumanState.uIdle)
        {
            if (animator_)
            {
                Vector3 pos = funeboko.transform.position;
                pos_ = Vector3.Lerp(pos_, pos, 0.075f);
                animator_.SetLookAtPosition(pos_);
                float weight_ = Mathf.Lerp(0f, 0.8f, t);
                t += 0.05f * Time.deltaTime;
                animator_.SetLookAtWeight(weight_, 0.5f, 0.7f, 1f, 0.6f);
            }
        }
    }
    void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }
    public void DestroyTimaki() {
        timaki.GetComponent<Renderer>().enabled = true;
        father.tag = "Missing";
    }
    public void AfterPickUp() {
        state = HumanState.Idle;
        stateMachine.Update();
    }

}