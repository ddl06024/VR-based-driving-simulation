using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 빨간불, 파란불, 노란불 이벤트 발생시킴
public class CarTrafficManager : MonoBehaviour
{

    public GameObject trafficsign;
    public CubeTrigger cubeTrigger;
    public CubestartTrigger cubestartTrigger;


    void Start()
    {
        cubeTrigger = gameObject.GetComponent<CubeTrigger>();
    }

    public void Yellow() 
    {
        //Debug.Log("Yellow");
        if ((cubeTrigger.carlist != null) && (cubestartTrigger.flag == 1) ) 
        {
            cubestartTrigger.npcCarController.movementSpeed = 0;
        }


    }

    public void Red()
    {
        //Debug.Log("Red");
        if ((cubeTrigger.carlist != null) && (cubestartTrigger.flag == 1))
        {
            cubestartTrigger.npcCarController.movementSpeed = 0;
        }
    }


    
    public void Green()
    {
        //Debug.Log("Green");
        if ((cubeTrigger.carlist != null))
        {
            for (int i = 0; i < cubeTrigger.carlist.Count; i++) 
            {
                NpcCarController npccarcontroller;
                npccarcontroller = cubeTrigger.carlist[i].GetComponent<NpcCarController>();
                npccarcontroller.movementSpeed = Random.Range(5f, 7f);
                //Debug.Log(npccarcontroller.movementSpeed);
            }
            
        }
        

    }

    public void get_sign()
    {
        if (trafficsign.transform.Find("PREFAB_trafficLight").transform.GetChild(3).gameObject.activeSelf == true)
        {          
            Yellow();
        }
        else if (trafficsign.transform.Find("PREFAB_trafficLight").transform.GetChild(4).gameObject.activeSelf == true)
        {
            Red();
        }
        else if (trafficsign.transform.Find("PREFAB_trafficLight").transform.GetChild(5).gameObject.activeSelf == true)
        {
            Green();
        }

    }

    void Update()
    {
        get_sign();
    }

}