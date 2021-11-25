using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorInvisible : MonoBehaviour
{
    //SDKInputManager가 객체에 mirrorInvisible에 함께 있어야함
   // public GameObject playercar;
    private SDKInputManager IM;
    private bool Tinput;
    private bool alreadyPressed = false;
    private Canvas canvas;
    
    // Start is called before the first frame update
    void Start()
    {
 
        IM = GetComponent<SDKInputManager>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        //canvas.planeDistance = 1000;


    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"인풋값 {IM.TInput}");
        if (IM.TInput == true) 
        {
            //print($"인풋값{IM.TInput}");
            IsPressedTbutton();
        }
    }

    public void IsPressedTbutton() 
    {
        if(alreadyPressed == false) 
        {
            canvas.enabled = true;
            Debug.Log($"미러 보이게");
          
        }
        else 
        {
            canvas.enabled = false;
            //gameObject.SetActive(true);
            Debug.Log($"미러 안보이게");
        }
        alreadyPressed = !alreadyPressed;

    }
}
