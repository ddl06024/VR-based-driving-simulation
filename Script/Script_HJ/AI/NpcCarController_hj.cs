using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCarController_hj : MonoBehaviour
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

    [Header("Variables")]
    public float torque; //바퀴굴리는 힘
    public float steeringMax = 10; // 바퀴 회전각
    public float totalPower = 0.0f;
    public float wheelsPRM;
    public float smoothTime = 0.01f;

    private float radius = 6; //바퀴 사이즈
    private float brakePow = 1000;
    public bool is_brake = false;
    public float DownForceValue = 1000; // 자동차 아래로 누르는 힘

    // waypoint navigation에 대한 변수
    public float movementSpeed;
    public float rotationSpeed = 120;
    public float stopDistance = 2f;
    public bool reachedDestination;
    public float fwdDotProcduct;
    public float rightDotProduct;

    public Vector3 destination, lastPosition, velocity;



    public void Awake()
    {
        //움직이는 속도 랜덤값 주기
        movementSpeed = Random.Range(5f, 20f);
        // Animation 갖고오기

        getObjects();
        
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


    
    private void OnCollisionEnter(Collision collision)
    {
        
        
        if (collision.gameObject.tag == "Car") 
        {
            var obj = collision.gameObject;
            NpcCarController_hj mine = gameObject.GetComponent<NpcCarController_hj>();
            NpcCarController_hj othercar = collision.gameObject.GetComponent<NpcCarController_hj>();
            

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

        //wheelMeshes = wheelsobject.transform.GetChild(0);
        for (int i = 0; i < 4; i++) 
        {
            wheels[i] = wheelsobject.transform.GetChild(1).transform.GetChild(i).gameObject.GetComponent<WheelCollider>();
        }
        //wheels[0] = wheelsobject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<WheelCollider>();
        //wheels[1] = wheelsobject.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<WheelCollider>();
        //wheels[2] = wheelsobject.transform.GetChild(1).transform.GetChild(2).gameObject.GetComponent<WheelCollider>();
        //wheels[3] = wheelsobject.transform.GetChild(1).transform.GetChild(3).gameObject.GetComponent<WheelCollider>();

        //wheelMeshes = wheelsobject.transform.GetChild(0);
        for (int i = 0; i < 4; i++) 
        {
            wheelMesh[i] = wheelsobject.transform.GetChild(0).transform.GetChild(i).gameObject;

        }
        //wheelMesh[0] = wheelMeshes.transform.Find("FrontLeftWheel").gameObject;
        //wheelMesh[1] = wheelMeshes.transform.Find("FrontRightWheel").gameObject;
        //wheelMesh[2] = wheelMeshes.transform.Find("RearLeftWheel").gameObject;
        //wheelMesh[3] = wheelMeshes.transform.Find("RearRightWheel").gameObject;

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

        //animator.SetFloat("right_left", rightDotProduct);
        // animator.SetFloat("farward", fwdDotProcduct);

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

        /*
        for(int i = 0; i < 2; i++) //앞바퀴만 회전한다.
        {
            wheels[i].steerAngle = steeringMax * IM.horizontal; //여기도 바퀴와 콜라이더가 직각인사람은 + 90을 해줘야한다.
        }
        */

        // acerman steering formula -> 이 공식을 사용하면 위에 코드보다 왼쪽, 오른쪽의 이동이 자연스럽다.
        // 왼쪽 오른쪽 이동할 때 바퀴 회전각을 다르게 해준다. 
        // steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f/(radius + (1.5f/2))) * horiaontalInput;
        

        if (rightDotProduct > 0)
        {
            //rear tracks size is set to 1.5f
            //wheel base has been set to 2.55f
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
        
        /*Vector3 relativeVector = transform.InverseTransformPoint(currentNode.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * 2;
        horizontal = newSteer;
        foreach (var item in wheels)
        {
            item.motorTorque = totalPower;
        }

        if (horizontal > 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        }
        else if (horizontal < 0)
        {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        }
        else
        {
            wheels[0].steerAngle = 0;
            wheels[1].steerAngle = 0;
        }
        */

    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

}