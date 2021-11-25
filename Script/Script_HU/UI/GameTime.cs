using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{

    public static int min;
    public static int sec;
    public static float mill;
    public static string display;

    public GameObject minui;
    public GameObject secui;
    //public GameObject millui;


    // Update is called once per frame
    void Update()
    {
        mill += Time.deltaTime * 10;
        display = mill.ToString("F0");
        //millui.GetComponent<Text>().text = "" + millui;

        if (mill >= 10)
        {
            mill = 0;
            sec += 1;                
        }

        if (sec <= 9)
        {
            secui.GetComponent<Text>().text = "0" + sec;
        }
        else 
        {
            secui.GetComponent<Text>().text = "" + sec;
        }
        if (sec >= 60) 
        {
            sec = 0;
            min += 1;
        }
        if (min <= 9)
        {
            minui.GetComponent<Text>().text = "0" + min + ":";
        }
        else 
        {
            minui.GetComponent<Text>().text = "" + min + ":";
        }
    }
}
