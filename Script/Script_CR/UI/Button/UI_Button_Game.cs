using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_Game : UI_Popup
{
    public GameObject panel;
    enum Buttons
    {
        PauseButton,

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
        BindEvent(GetButton((int)Buttons.PauseButton).gameObject, OnPauseButtonClicked);

    }

    public void OnPauseButtonClicked(PointerEventData data)
    {
        panel.SetActive(true);
        //게임 일시정지 기능 추가
    }
}
