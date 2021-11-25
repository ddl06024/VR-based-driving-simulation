using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Button_Menu : UI_Popup
{
    enum Buttons
    {
        GameButton,
        MyPageButton,
        LogOutButton,
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
        BindEvent(GetButton((int)Buttons.GameButton).gameObject, OnGameButtonClicked);
        BindEvent(GetButton((int)Buttons.MyPageButton).gameObject, OnMyPageButtonClicked);
        BindEvent(GetButton((int)Buttons.LogOutButton).gameObject, OnLogOutButtonClicked);

    }

    public void OnGameButtonClicked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.SelectMap);
    }
    public void OnMyPageButtonClicked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.MyPage);
    }
    public void OnLogOutButtonClicked(PointerEventData data)
    {
        //로그인 정보 제거되는 기능 추가
        Managers.Scene.LoadScene(Define.Scene.Login);
    }
}
