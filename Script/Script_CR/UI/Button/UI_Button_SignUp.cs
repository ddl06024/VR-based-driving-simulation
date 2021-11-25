using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_SignUp : UI_Popup
{
    enum Buttons
    {
        SignUpButton,
    }

    enum InputField
    {
        Name_InputField,
        Email_InputField,
        NickName_InputField,
        ID_InputField,
        PW_InputField,
        PW_Re_InputField,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); //enum 이름은 Buttons이고 Button의 컴포넌트를 갖는 객체를 찾을 것

        //GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnButtonClicked);
        BindEvent(GetButton((int)Buttons.SignUpButton).gameObject, OnSignUpButtonClicked);

    }

    public void OnSignUpButtonClicked(PointerEventData data)
    {
        //회원가입 정보 저장하는 기능 추가
        Managers.Scene.LoadScene(Define.Scene.Login);
    }
}
