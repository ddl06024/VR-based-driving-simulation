using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_SelectCar : UI_Popup
{
    public Slider slider;

    enum Buttons
    {
        GameStartButton,
    }

    enum Images
    {
        Car1Image,
        Car2Image,
        Car3Image,

    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); //enum 이름은 Buttons이고 Button의 컴포넌트를 갖는 객체를 찾을 것
        Bind<Image>(typeof(Images));
        //GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnButtonClicked);
        BindEvent(GetButton((int)Buttons.GameStartButton).gameObject, OnGameStartButtonClicked);
        BindEvent(slider.gameObject, OnValueChanged);
    }

    public void OnGameStartButtonClicked(PointerEventData data)
    {
        //선택한 난이도 정보 전달하는 기능 추가
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
    public void OnValueChanged(PointerEventData data)
    {
        float value = slider.value;
        switch (value)
        {
            case 0:
                GetImage((int)Images.Car1Image).gameObject.SetActive(true);
                GetImage((int)Images.Car2Image).gameObject.SetActive(false);
                GetImage((int)Images.Car3Image).gameObject.SetActive(false);
                break;
            case 1:
                GetImage((int)Images.Car1Image).gameObject.SetActive(false);
                GetImage((int)Images.Car2Image).gameObject.SetActive(true);
                GetImage((int)Images.Car3Image).gameObject.SetActive(false);
                break;
            case 2:
                GetImage((int)Images.Car1Image).gameObject.SetActive(false);
                GetImage((int)Images.Car2Image).gameObject.SetActive(false);
                GetImage((int)Images.Car3Image).gameObject.SetActive(true);
                break;

        }
    }

}
