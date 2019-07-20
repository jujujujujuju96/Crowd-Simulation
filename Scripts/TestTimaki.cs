/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTimaki : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("Space");
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(0.0f,0.0f,1.0f);
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Space");
            GetComponent<Rigidbody>().AddForce(vec*100.0f,ForceMode.Force);
        }
    }
}*/
using UnityEngine;
using System.Collections;
public class TestTimaki : MonoBehaviour
{
    public float Power = 0.1f;//这个代表发射时的速度/力度等，可以通过此来模拟不同的力大小
    public float Angle = 45;//发射的角度，这个就不用解释了吧
    public float Gravity = -10;//这个代表重力加速度
    public GameObject obj;

    private Vector3 MoveSpeed;//初速度向量
    private Vector3 GritySpeed = Vector3.zero;//重力的速度向量，t时为0
    private float dTime;//已经过去的时间
    private Vector3 currentAngle;
    private Rigidbody rigidbody;
    private Vector3 vec = new Vector3(0,0,1);
    private bool startCondition = false;
    public bool destoyFlag = false;
    // Use this for initialization
    void Start()
    {
        //通过一个公式计算出初速度向量
        //角度*力度w
        vec.x = Random.Range(-0.7f,0.7f);
        Angle = Random.Range(-20.0f, 60.0f);
        MoveSpeed = Quaternion.Euler(new Vector3(0, 0, Angle)) * vec * Power;
        currentAngle = Vector3.zero;
        rigidbody = this.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space")) {
            Debug.Log("Space");
            startCondition = true;
        }
        if (destoyFlag == true) {
            Destroy(this);
        }
    }
    void FixedUpdate()
    {
        //计算物体的重力速度
        //v = at ;
        if (transform.position.y > 0.15f && startCondition == true)
        {
            GritySpeed.y = Gravity * (dTime += Time.fixedDeltaTime);
            //位移模拟轨迹
            transform.position += (MoveSpeed + GritySpeed) * Time.fixedDeltaTime;
            currentAngle.z = Mathf.Atan((MoveSpeed.y + GritySpeed.y) / MoveSpeed.x) * Mathf.Rad2Deg;
            transform.eulerAngles = currentAngle;
        }
        if (transform.position.y < 0.6f) {
            obj.GetComponent<transform_kominka>().enabled = false;
        }
    }
}
