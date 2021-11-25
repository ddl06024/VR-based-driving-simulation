using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navi_turn_right : MonoBehaviour
{
    public GameObject Box;
    private BoxCollider boxcolliders;
    public GameObject playercar;
    public GameObject sign;
    public Sprite[] sign_ui;


    // Start is called before the first frame update
    void Start()
    {
        //Box = gameObject;
        boxcolliders = Box.GetComponent<BoxCollider>();
        sign_ui = Resources.LoadAll<Sprite>("Sign_ui");
        sign.SetActive(false);
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            //Debug.Log("들어옴");
            sign.GetComponent<Image>().sprite = sign_ui[2];
            sign.SetActive(true);
            //Debug.Log("우회잔");
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("나감");
            sign.SetActive(false);
        }

    }

    void Update()
    {
         
    }
}
