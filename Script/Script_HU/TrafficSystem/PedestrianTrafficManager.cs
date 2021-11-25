using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 신호등 보고 차, 보행자 waypoint조절
public class PedestrianTrafficManager : MonoBehaviour
{
    public GameObject pedestriantrafficsign;
    public GameObject[] trafficWaypoints;
    

    public void get_sign()
    {
        // 보행자신호등 빨간불

        if (pedestriantrafficsign.transform.GetChild(3).gameObject.activeSelf == true)
        {

            //Debug.Log(string.Format("getchild3 {0}", pedestriantrafficsign.transform.GetChild(3).name));
            remove_branch();
           // Debug.Log("보행자신호등 빨간불");
        }
        /*
        else if (pedestriantrafficsign.transform.GetChild(4).gameObject.activeSelf == true)
        {
            remove_branch();
        }
        */

    }

    void Update()
    {
        get_sign();
    }


    // waypoint를 돌면서 branch를 찾아내고 제거한다. 
    public void remove_branch() 
    {

        for (int j = 0; j < trafficWaypoints.Length; j++)
        {

            Waypointer waypointer = trafficWaypoints[j].GetComponent<Waypointer>();
            waypointer.branchRatio = 0;
        }
       
        
    }


}
