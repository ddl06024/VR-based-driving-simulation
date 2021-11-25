using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_Login : UI_Popup
{
    public GameObject ErrPanel;

    enum Buttons
    {
        LoginButton,
    }

    enum InputFields
    {
        IDInputField,
        PWInputField,
    }

    enum Texts
    {
        LoginErrText,
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));

        BindEvent(GetButton((int)Buttons.LoginButton).gameObject, OnLoginButtonClicked);
    }

    IEnumerator SetPanel()
    {
        ErrPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        ErrPanel.SetActive(false);
        Managers.Scene.LoadScene(Define.Scene.Login);
    }

    public void OnLoginButtonClicked(PointerEventData data)
    {
        //로그인 정보 db 전달
        string id = GetInputField((int)InputFields.IDInputField).text.ToString();
        string pw = GetInputField((int)InputFields.PWInputField).text.ToString();

        ErrPanel.SetActive(true);
        Bind<TMP_Text>(typeof(Texts));

        if (id == "")
        {
            Debug.Log("enter your id");
            GetText((int)Texts.LoginErrText).text = "Enter your ID";
        }
        else if (pw == "")
        {
            Debug.Log("enter your password");
            GetText((int)Texts.LoginErrText).text = "Enter your PW";
        }


        //id pw 전달
        user_info.User_Info.Set_id(id);
        user_info.User_Info.Set_password(pw);

        StartCoroutine(SetPanel());

        bool login = Database.db.Login();
        if (login)
        {
            ErrPanel.SetActive(false);
            Managers.Scene.LoadScene(Define.Scene.Menu);
            return;
        }

        else
        {
            Debug.Log("Failed to Login");
            GetText((int)Texts.LoginErrText).text = "Failed to Login";
        }
    }
}
