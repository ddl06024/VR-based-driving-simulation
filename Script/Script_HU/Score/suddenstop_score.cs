using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suddenstop_score : MonoBehaviour
{
    //드래그드랍해줘야함
    public GameObject playercar;
    private SDKInputManager IM;
    private float lastbrakeInput;
    private bool onetimededuction = false;
    public int totalscore = 100;

    void Start()
    {
        IM = playercar.GetComponent<SDKInputManager>();

    }


    void Update()
    {      
        suddenstop();
    }

    public void suddenstop() 
    {
        // 브레이크 안밟으면 -1 ~ 밟으면 1
        float degree_brake = ((IM.brake - lastbrakeInput) / Time.deltaTime);
        lastbrakeInput = IM.brake;
        if (degree_brake >= 15)
        {
            if (!onetimededuction)
            {
                //scoretest_hj.stagePoint -= 5;
                totalscore -= 10;
                print("브레이크점수감점");
                onetimededuction = !onetimededuction;
            }

        }
        else if (degree_brake < 15) 
        {
            if (onetimededuction)
            {
                onetimededuction = !onetimededuction;
            }
        }
    }

}
