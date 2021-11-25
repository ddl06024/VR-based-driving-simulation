using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeyboard.Utils;

public class InputFieldManager : MonoBehaviour
{ 

    public GameObject Keyboard;
    public KeyboardManager keyboardM;
    void Start()
    {
        Keyboard = GameObject.FindGameObjectWithTag("Keyboard");
        keyboardM =  Keyboard.GetComponent<KeyboardManager>();

    }

    void Update()
    {
    }

    public void selectInputField() 
    {
        Debug.Log($"InputField Selected: {transform.gameObject.name}");
        Debug.Log($"Keyboard GameObject: {Keyboard}");
        Debug.Log($"KeyboardManager : {keyboardM}");

        keyboardM.InputObject = transform.gameObject;
    }
}
