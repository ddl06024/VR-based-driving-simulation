using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCarController: MonoBehaviour
{
    // 자동차에 대한 변수
    public WheelCollider[] wheels = new WheelCollider[4];
    public GameObject[] wheelMesh = new GameObject[4];
    private Rigidbody rb;
    // 툴에 있는 wheelColliders와 wheelMeshes
    private Transform wheelColliders, wheelMeshes;
    private GameObject wheelsobject;
    // 자동차 무게중심을 최대한 낮춤.
    private GameObject centerofMass;
    // 자동차 마다 랜덤하게 수를 배정 -> 차 두대 충돌시 높은 수 차를 먼저 보냄.
    public float ranking;
    public int flag = 0;

    [Header("Variables")]
    public float torque; //바퀴굴리는 힘
    public float steeringMax = 10; // 바퀴 회전각
    public float totalPower = 0.0f;
    public float wheelsPRM;
    public float smoothTime = 0.01f;

    private float radius = 6; //바퀴 사이즈
    private float brakePow = 1000;
    public bool is_brake = false;
    public float DownForceValue = 50; // 자동차 아래로 누르는 힘

    // waypoint navigation에 대한 변수
    public float movementSpeed;
    public float rotationSpeed = 120;
    public float stopDistance = 2f;
    public bool reachedDestination;
    public float fwdDotProcduct;
    public float rightDotProduct;

    public Vector3 destination, lastPosition, velocity;

    public float timer;
    public float waitingtime;



    public void Awake()
    {

        //움직이는 속도 랜덤값 주기
        movementSpeed = Random.Range(6f, 7f);
        // Animation 갖고오기

        getObjects();
        ranking = Random.Range(0f, 100f);
        timer = 0f;
        waitingtime = 4f;
    }

    

    public void Update()
    {
        if (transform.position != destination)
        {
            Settings();
            addDownForce();
            animateWheel();
            moveVehicle();
            steerVehicle();
            lastPosition = transform.position;
            // 앞차랑 부딪히면 속도 줄이기
            // 신호 받고 아파랑 부딪히면 속도 멈추기
            //collider_redsign();
        }

    }
    private void OnCollisionrEnter(Collision collision) 
    {
        //NpcCarController mine = gameObject.GetComponent<NpcCarController>();
        NpcCarController othercar = collision.gameObject.GetComponent<NpcCarController>();
        NpcCarController mine = gameObject.GetComponent<NpcCarController>();

        
        if (collision.gameObject.tag == "Car") 
        {
            if (mine.movementSpeed == 0) 
            {
                othercar.movementSpeed = 0;
            }

            //othercar.GetComponent<Rigidbody>().AddForce(new Vector3(0, 50, 0));

        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        NpcCarController othercar = collision.gameObject.GetComponent<NpcCarController>();
        if (collision.gameObject.tag == "Car") 
        {
            othercar.movementSpeed = 6f;
        }
            
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Car") 
        {
            NpcCarController mine = gameObject.GetComponent<NpcCarController>();
            NpcCarController othercar = collision.gameObject.GetComponent<NpcCarController>();
           
            timer += Time.deltaTime;
            if (timer < waitingtime)
            {
                if (mine.ranking > othercar.ranking)
                {
                    othercar.movementSpeed = 0;
                    flag = 0;
                }

                else 
                {
                    mine.movementSpeed = 0;
                    flag = 1;
                }
            }

            if (mine.flag == 1) 
            {
                mine.movementSpeed = Random.Range(5f, 7f);
            }
            else
            {
                othercar.movementSpeed = Random.Range(5f, 7f);
            }

        }

    }
    private void OnCollisionExit(Collision collision) 
    {
        timer = 0;
    }
    */


    //Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), obj.GetComponent<Collider>());
    /*
    private void OnTriggerEnter(Collider collision)
    {
        
        
        if (collision.gameObject.tag == "Car") 
        {
            var obj = collision.gameObject;
            NpcCarController mine = gameObject.GetComponent<NpcCarController>();
            NpcCarController othercar = collision.gameObject.GetComponent<NpcCarController>();
            

            if ((mine.movementSpeed == 0) || (othercar.movementSpeed == 0))
            {
                othercar.movementSpeed = 0;
                mine.movementSpeed = 0;
            }
            
            else 
            {
                if (mine.movementSpeed != othercar.movementSpeed)
                {
                    mine.movementSpeed = othercar.movementSpeed ;
                }
                
            }

            


            Debug.Log("충돌 시작!");
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), obj.GetComponent<Collider>());

        }

    }
    */

    /*
    private void collider_redsign() 
    {
    
    }
    */
    private void getObjects()
    {
        rb = GetComponent<Rigidbody>(); //리지드바디를 받아온다.
        centerofMass = GameObject.Find("mass"); // 차마다 mass 오브젝트 추가하고 낮게 설정해준다. 
        rb.centerOfMass = centerofMass.transform.localPosition;
        //private GameObject wheelsobject = GameObject.Find("Wheels");
        wheelsobject = gameObject.transform.GetChild(1).gameObject;


    }

    public void Settings() 
    {
        // 목적지로의 벡터를 구함
        Vector3 destinationDirection = destination - transform.position;
        destinationDirection.y = 0;
        // 목적지까지의 거리 변수
        float destinationDistance = destinationDirection.magnitude;
        if (destinationDistance >= stopDistance)
        {
            reachedDestination = false;
            // destinationDirectin으로 방향 회전하고, 이동
            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            reachedDestination = true;
        }
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        velocity.y = 0;
        // velocityMagnitude는 velocity의 크기를 갖는다
        float velocityMagnitude = velocity.magnitude;
        //velocity는 방향 벡터
        velocity = velocity.normalized;
        float fwdDotProcduct = Vector3.Dot(transform.forward, velocity);
        float rightDotProduct = Vector3.Dot(transform.right, velocity);


    }
    private void addDownForce()
    {
        rb.AddForce(-transform.up * DownForceValue * rb.velocity.magnitude);
    }

    // 바퀴 pos, rot 바꾸기.
    void animateWheel()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheels[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMesh[i].transform.position = wheelPosition;
            wheelMesh[i].transform.rotation = wheelRotation;
        }
    }

    private void moveVehicle()
    {
        for (int i = 0; i < 4; i++)
        {
            totalPower += torque * movementSpeed;
            wheels[i].motorTorque = torque * movementSpeed; //바퀴를 굴린다.
        }
        if (is_brake == true)
        {
            wheels[2].brakeTorque = wheels[3].brakeTorque = brakePow;
        }
        else 
        {
            wheels[2].brakeTorque = wheels[3].brakeTorque = 0;
        }
      
    }

    private void steerVehicle()
    {

        if (rightDotProduct > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * rightDotProduct;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * rightDotProduct;
        }
        else if (rightDotProduct < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * rightDotProduct;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * rightDotProduct;

        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

}