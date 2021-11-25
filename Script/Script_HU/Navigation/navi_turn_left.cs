using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class navi_turn_left : MonoBehaviour
{
    public GameObject Box;
    private BoxCollider boxcolliders;
    public GameObject playercar;
    public GameObject sign;
    private Sprite[] sign_ui;


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
            sign.GetComponent<Image>().sprite = sign_ui[1];
            sign.SetActive(true);
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
