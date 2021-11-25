using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// vertical, horizontal  
/// accelInput, clutchInput, brakeInput, steerInput, gearInput(14 전진, 15 후진, no input Neutral Gear)
/// XInput, OInput, SInput, TInput, LeftBumperInput
/// </summary>
public class Controller : MonoBehaviour
{
    private SDKInputManager IM; // 들어오는 모든 input

    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheelMeshes = new GameObject[4];
    // 왼쪽앞, 왼쪽뒤, 오른쪽앞, 오른쪽뒤
    public GameObject[] Indicatorlightobject = new GameObject[4];
    private Light[] Indicatorlights = new Light[4];
    //private int Indicatorlightleftflag = 0;
    //private int Indicatorlightrightflag = 0;

    private Rigidbody rb;
    public GameObject mass;

    public float brakePower = 500;
    public float downforce = 50;
    public float radius = 6;
    public float torque = 250.0f;
    //public float torque = 50.0f;

    private bool isPlaying = false;
    private bool isFirstPlaying = true;
    private bool passStartMiss = false;
    private float speed = 0.0f;
    float speedavg = 0.0f;

    // Sound를 위한 변수
    public int currentGear = 0;

    private float Pitch;
    private float PitchDelay;

    private float shiftTime = 0.0f;
    private float shiftDelay = 0.0f;

    bool NeutralGear = false;
    bool BackGear = false;
    bool brake = false;

    public float motorRPM = 0.0f;
    private float accel = 0.0f;
    private float steer = 0.0f;

    // light를 위한 변수 
    private bool Indicatorlightleftflag = false;
    private bool LeftSecondflag = false;
    private bool Indicatorlightrightflag = false;
    private bool RightSecondflag = false;

    // CarSetting ////////////////////////
    public float carPower = 120f;
    //public float brakePower = 500f;

    public float LimitBackwardSpeed = 60.0f;
    public float LimitForwarSpeed = 100.0f;

    //RPM
    public float shiftDownRPM = 1500;
    public float shiftUpRPM = 2500;
    public float idleRPM = 500;

    public float[] gears = { -10.0f, 4.5f, 2.5f };

    // Lights Setting ///////////////////
    public CarLights carLights;

    [System.Serializable]
    public class CarLights
    {
        public Light[] brakeLights;
        public Light[] reverseLights;
    }

    /* CarSetting     
    public CarSetting carSetting;

    [SerializeField]

    public class CarSetting
    {
        public float carPower = 120f;
        public float brakePower = 500f;

        public float LimitBackwardSpeed = 60.0f;
        public float LimitForwarSpeed = 100.0f;

        //RPM
        public float shiftDownRPM = 1500.0f;
        public float shiftUpRPM = 2500.0f;
        public float idleRPM = 500.0f;

        public float[] gears = { -10.0f, 4.5f, 2.5f };

    }
    */

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        /*carSetting Debugging
        Debug.Log($"idleRPM: {idleRPM}");
        Debug.Log($"shiftDownRPM: {shiftDownRPM}");
        Debug.Log($"shiftUpRPM: {shiftUpRPM}");
        */
        //onPlayStart 시, AddListener 추가 
        getObjects();
        GameObject.Find("@PlayState").GetComponent<PlayState>().onPlayStart.AddListener(OnPlayStart);
        Debug.Log($"playstate {GameObject.Find("@PlayState")}");

        isPlaying = false;
    }

    public void OnPlayStart()
    {
        isPlaying = true;
    }
    void Update()
    {
        Debug.Log("update");

        Debug.Log($"IM.LeftBumperInput {IM.LeftBumperInput}");

        if (IM.LeftBumperInput)
        {

            if (!LeftSecondflag)
                Indicatorlightleftflag = true;
            else Indicatorlightleftflag = false;

            LeftSecondflag = !LeftSecondflag;
        }

        if (Indicatorlightleftflag)
        {
            Debug.Log("blink");

            Invoke("Leftlightson", 1.0f);
            Invoke("offlights", 2.0f);
        }
        else offlights();


        if (IM.RightBumperInput)
        {

            if (!RightSecondflag)
                Indicatorlightrightflag = true;
            else Indicatorlightrightflag = false;

            RightSecondflag = !RightSecondflag;
        }

        if (Indicatorlightrightflag)
        {
            Debug.Log("blink");

            Invoke("Rightlightson", 1.0f);
            Invoke("offlights", 2.0f);
        }
        else offlights();



    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlaying)
        {
            speed = rb.velocity.magnitude * 2.7f;
            Debug.Log($"speed: {speed}");

            // 시작 후, 처음일때, 처음 3초 동안 Speed 계산
            if (isFirstPlaying)
            {
                StartCoroutine(TimeCoroutine());

                // Speed 계산하게 함
                DownForce(); // 중력 조정
                ShiftGear(); // 기어 조정 
                animateWheels();
                moveVehicle();
                steerVehicle();
                speed = rb.velocity.magnitude * 2.7f;
                speedavg += speed;

            }
            else if (!passStartMiss)
            {
                if (speedavg / 3.0f < 0.5)
                    Managers.Score.ScoreDeduct(Define.ScoreDeduct.StartMiss);

                passStartMiss = !passStartMiss;
            }

            DownForce(); // 중력 조정
            ShiftGear(); // 기어 조정 
            animateWheels();
            moveVehicle();
            steerVehicle();
        }
    }
    IEnumerator TimeCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        isFirstPlaying = false;
    }

    // GearChange
    private void ShiftGear()
    {
        float now = Time.timeSinceLevelLoad;

        if (now < shiftDelay) return;

        else if (currentGear != IM.gearInput)
        {
            //Managers.Sound.Play("Car/SwitchGear1", Define.Sound.SwitchGear);

            shiftDelay = now + 1.0f;
            shiftTime = 1.5f;
            currentGear = IM.gearInput;

        }

    }

    private void moveVehicle()
    {
        
        // Check Speed
        speed = rb.velocity.magnitude * 2.7f;

        Debug.Log($"speed: {speed}");
        
        NeutralGear = (IM.gearInput == 1);
        BackGear = (IM.gearInput == 2);
        brake = (IM.brakeInput == -1);
        accel = IM.vertical;
        steer = IM.horizontal;

        float rpm = 0.0f;
        int motorizedWheels = 0;
        int currentWheel = 0; 

        foreach (WheelCollider wc in wheelColliders)
        {
            Debug.Log("rpm 계산");
            // idle
            if (!NeutralGear && brake)
            {
                rpm += idleRPM * accel;
            }
            else
            {
                if (!NeutralGear)
                    rpm += wc.rpm;
                else
                    rpm += idleRPM * accel;
            }

            motorizedWheels++;
            Debug.Log($"RPM: {rpm}");

        }

        if (motorizedWheels > 1)
            rpm /= motorizedWheels;

        motorRPM = 0.95f * motorRPM + 0.05f * Mathf.Abs(rpm * gears[currentGear]);
        if (motorRPM > 5500.0f) motorRPM = 5200.0f;

        Pitch = Mathf.Clamp(1.2f + ((motorRPM - idleRPM) / (shiftUpRPM - idleRPM)), 1.0f, 10.0f);
        shiftTime = Mathf.MoveTowards(shiftTime, 0.0f, 0.1f);

        float Idlevolume = 0.0f;
        float Lowvolume = 0.0f;
        float Highvolume = 0.0f;

        Debug.Log($"Pitch: {Pitch}");
        if (Pitch == 1)
        {
            //Managers.Sound.Play("Car/idle", Define.Sound.Idle, 1, Mathf.Lerp(Idlevolume, 1.0f, 0.1f));
            //Managers.Sound.Play("Car/low_on", Define.Sound.LowEngine, 1, Mathf.Lerp(Lowvolume, 1.0f, 0.1f));
            //Managers.Sound.Play("Car/high_on", Define.Sound.HighEngine, 1, Mathf.Lerp(Highvolume, 0.0f, 0.1f));
        }
        else
        {
            //Managers.Sound.Play("Car/idle", Define.Sound.Idle, 1, Mathf.Lerp(Idlevolume, 1.8f - Pitch, 0.1f));

            if((Pitch > PitchDelay || accel > 0 ) && shiftTime == 0.0f)
            {
                //Managers.Sound.Play("Car/low_on", Define.Sound.LowEngine, 1, Mathf.Lerp(Lowvolume, 0.0f, 0.2f));
                //Managers.Sound.Play("Car/high_on", Define.Sound.HighEngine, 1, Mathf.Lerp(Highvolume, 1.0f, 0.1f));
            }
            else
            {
                //Managers.Sound.Play("Car/low_on", Define.Sound.LowEngine, 1, Mathf.Lerp(Lowvolume, 0.5f, 0.1f));
                //Managers.Sound.Play("Car/high_on", Define.Sound.HighEngine, 1, Mathf.Lerp(Highvolume, 0.0f, 0.2f));

            }
            PitchDelay = Pitch;
        }
        

        /* keyboard 입력을 위한 디버깅 코드
        if (IM.vertical > 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].motorTorque = ((IM.vertical + 1) / 2) * torque;
            }
        }
        else if (IM.vertical <= 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].motorTorque = ((IM.vertical + 1) / -2) * torque;
            }
        }
        // 기어 상관 없이 브레이크 설정
        if (IM.brakeInput >= 0.2)
        {
            wheelColliders[0].brakeTorque = (IM.brakeInput) * brakePower;
            wheelColliders[1].brakeTorque = (IM.brakeInput) * brakePower;
        }
        else
        {
            wheelColliders[0].brakeTorque = 0;
            wheelColliders[1].brakeTorque = 0;
        }

        */


        // Logitech을 사용한 코드
        if (IM.gearInput == 1)
        {
            Debug.Log("go!");
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                Debug.Log($"Vertical {IM.accelInput}");
                wheelColliders[i].motorTorque = ((accel + 1) / 2) * torque;
            }
        }

        // 기어가 뒤로 가면 후진
        if (IM.gearInput == 2)
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].motorTorque = ((accel + 1) / -2) * torque;
            }
        }

        Debug.Log($"brake Input: {IM.brakeInput}");
        wheelColliders[0].brakeTorque = (IM.brakeInput + 1) * 5 * brakePower;
        wheelColliders[1].brakeTorque = (IM.brakeInput + 1) * 5 * brakePower;
        wheelColliders[2].brakeTorque = (IM.brakeInput + 1) * 5 * brakePower;
        wheelColliders[3].brakeTorque = (IM.brakeInput + 1) * 5 * brakePower;
        

    }

    // 바퀴 회전
    void animateWheels()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheelMeshes[i].transform.position = wheelPosition;
            wheelMeshes[i].transform.rotation = wheelRotation;

        }

    }

    private void steerVehicle()
    {
        if (IM.horizontal > 0)
        {
            Debug.Log($"바퀴회전{IM.horizontal}");
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan2(2.55f, (radius - (1.5f / 2))) * IM.horizontal * 1.5f;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan2(2.55f, (radius + (1.5f / 2))) * IM.horizontal * 1.5f;
        }
        else if (IM.horizontal < 0)
        {
            wheelColliders[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan2(2.55f, (radius + (1.5f / 2))) * IM.horizontal * 1.5f;
            wheelColliders[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan2(2.55f, (radius - (1.5f / 2))) * IM.horizontal * 1.5f;
        }
        else
        {
            wheelColliders[0].steerAngle = 0;
            wheelColliders[1].steerAngle = 0;
        }
    }

    private void getObjects()
    {
        IM = GetComponent<SDKInputManager>();

        rb = GetComponent<Rigidbody>();
        mass = GameObject.Find("mass");
        rb.centerOfMass = mass.transform.localPosition;

        Debug.Log($"IM: {IM}, rb: {rb}, mass: {mass}");

        Debug.Log($"IM.gearInput: {IM.gearInput}");

        for (int i = 0; i < 4; i++)
        {
            Indicatorlights[i] = Indicatorlightobject[i].GetComponent<Light>();
            //Indicatorlights[i].SetActive(false);
        }

    }

    private void DownForce()
    {
        rb.AddForce(-transform.up * downforce * rb.velocity.magnitude);
    }

    private void offlights()
    {
        Debug.Log("왼쪽 범퍼 켜져있어서 끄기");
        Indicatorlightobject[0].GetComponent<Light>().intensity = 0;
        Indicatorlightobject[1].GetComponent<Light>().intensity = 0;
        Indicatorlightobject[0].GetComponent<LensFlare>().brightness = 0;
        Indicatorlightobject[1].GetComponent<LensFlare>().brightness = 0;
        Indicatorlightobject[2].GetComponent<Light>().intensity = 0;
        Indicatorlightobject[3].GetComponent<Light>().intensity = 0;
        Indicatorlightobject[2].GetComponent<LensFlare>().brightness = 0;
        Indicatorlightobject[3].GetComponent<LensFlare>().brightness = 0;
    }


    private void Leftlightson()
    {
        Indicatorlightobject[0].GetComponent<Light>().intensity = 10;
        Indicatorlightobject[1].GetComponent<Light>().intensity = 10;
        Indicatorlightobject[0].GetComponent<LensFlare>().brightness = 1;
        Indicatorlightobject[1].GetComponent<LensFlare>().brightness = 1;
    }

    private void Rightlightson()
    {
        Indicatorlightobject[2].GetComponent<Light>().intensity = 10;
        Indicatorlightobject[3].GetComponent<Light>().intensity = 10;
        Indicatorlightobject[2].GetComponent<LensFlare>().brightness = 1;
        Indicatorlightobject[3].GetComponent<LensFlare>().brightness = 1;
    }
}