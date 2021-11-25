using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject BrakeLights;
    public GameObject FrontLights;
    public GameObject LeftIndicatorLights;
    public GameObject RightIndicatorLights;
    private NpcCarController npccarcontroller;
    private WaypointNavigator waypointnavigator;



    private float timer = 1f;
    //private float timer2 = 0.5f;
    //private float btimer = 0.5f;
    //private float btimer2 = 0.5f;


    void Start()
    {
        npccarcontroller = gameObject.GetComponent<NpcCarController>();
        waypointnavigator = gameObject.GetComponent<WaypointNavigator>();
        
        BrakeLights = GameObject.Find("BrakeLights");
        FrontLights = GameObject.Find("FrontLights");
        LeftIndicatorLights = GameObject.Find("LeftIndicatorLights");
        RightIndicatorLights = GameObject.Find("RightIndicatorLights");
        
        BrakeLights.SetActive(false);
        FrontLights.SetActive(true);
        LeftIndicatorLights.SetActive(false);
        RightIndicatorLights.SetActive(false);


    }
    /*
    void Update()
    {
        if (npccarcontroller.movementSpeed == 0)
        {
            BrakeLights.SetActive(true);
        }
        
        if (npccarcontroller.movementSpeed != 0)
        {
            BrakeLights.SetActive(false);
    }

        //left이면
        /*
        if (waypointnavigator.LeftRight == 1)
        {
            Debug.Log("자동차좌회전");
            LeftIndicatorLights.SetActive(true);
            if (timer >= 0f)
            {
                Debug.Log("깜빡");
                LeftIndicatorLights.SetActive(true);
                timer -= Time.deltaTime;
                timer2 = 0.5f;
            }

            if (timer <= 0f)
            {
                LeftIndicatorLights.SetActive(false);
                timer2 -= Time.deltaTime;
                if (timer2 <= 0f) timer = 0.5f;
            }
        }
     



        // left이면
        if (waypointnavigator.LeftRight == 1)
        {

            if (timer >= 0f)
            {
                Debug.Log("타임1");
                LeftIndicatorLights.SetActive(true);
                timer -= Time.deltaTime * 2;

            }

            if (timer < 0f)
            {
                Debug.Log("타임2");
                LeftIndicatorLights.SetActive(false);

            }

        }

        else 
        {
            Debug.Log("leftright");
            Debug.Log(waypointnavigator.LeftRight);
            timer = 1f;
        }





    }
       */
}
