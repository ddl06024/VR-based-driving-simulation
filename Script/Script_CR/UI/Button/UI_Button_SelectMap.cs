using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class UI_Button_SelectMap : UI_Popup
{
    public Toggle Normal_Toggle, School_Toggle, Park_Toggle, Mountain_Toggle; //수동으로 집어넣기
    public TMP_Dropdown Normal_Dropdown, School_Dropdown, Park_Dropdown, Mountain_Dropdown; //수동으로 집어넣기
    public Sprite Normal_Image, School_Image, Park_Image, Mountain_Image;
    int flag;

    bool check_d_n(int flag)
    {
        if (flag == 0) return true; //day이면 true
        else return false;
    }
    enum Buttons
    {
        NextButton,
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.NextButton).gameObject, OnNextButtonClicked);
        BindEvent(Normal_Toggle.gameObject, OnNomalPointerClicked);
        BindEvent(School_Toggle.gameObject, OnSchoolPointerClicked);
        BindEvent(Park_Toggle.gameObject, OnParkPointerClicked);
        BindEvent(Mountain_Toggle.gameObject, OnMountainPointerClicked);
    }



    public void OnNomalPointerClicked(PointerEventData data)
    {
        if (Normal_Toggle.isOn)
        {
            user_info.User_Info.Set_game_map("Normal");
            bool flag = check_d_n(Normal_Dropdown.value);
            user_info.User_Info.Set_day_or_night(flag);
            GameObject.Find("SelectMap").GetComponent<Image>().sprite = Normal_Image;
            School_Toggle.isOn = false;
            Park_Toggle.isOn = false;
            Mountain_Toggle.isOn = false;
        }
    }
    public void OnSchoolPointerClicked(PointerEventData data)
    {
        if (School_Toggle.isOn)
        {
            user_info.User_Info.Set_game_map("School");
            bool flag = check_d_n(School_Dropdown.value);
            user_info.User_Info.Set_day_or_night(flag);
            GameObject.Find("SelectMap").GetComponent<Image>().sprite = School_Image;
            Normal_Toggle.isOn = false;
            Park_Toggle.isOn = false;
            Mountain_Toggle.isOn = false;
        }
    }
    public void OnParkPointerClicked(PointerEventData data)
    {
        if (Park_Toggle.isOn)
        {
            user_info.User_Info.Set_game_map("Park");
            bool flag = check_d_n(Park_Dropdown.value);
            user_info.User_Info.Set_day_or_night(flag);
            GameObject.Find("SelectMap").GetComponent<Image>().sprite = Park_Image;
            Normal_Toggle.isOn = false;
            School_Toggle.isOn = false;
            Mountain_Toggle.isOn = false;
        }
    }

    public void OnMountainPointerClicked(PointerEventData data)
    {
        if (Mountain_Toggle.isOn)
        {
            user_info.User_Info.Set_game_map("Mountain");
            bool flag = check_d_n(Mountain_Dropdown.value);
            user_info.User_Info.Set_day_or_night(flag);
            GameObject.Find("SelectMap").GetComponent<Image>().sprite = Mountain_Image;
            Normal_Toggle.isOn = false;
            School_Toggle.isOn = false;
            Park_Toggle.isOn = false;
        }
    }

    public void OnNextButtonClicked(PointerEventData data)
    {
        user_info.User_Info.Set_game_number(user_info.User_Info.Get_last_game_number() + 1);
        if (Database.db.Set_Map_and_Day_or_Night())
        {
            Managers.Scene.LoadScene(Define.Scene.SelectDif);
        }
    }
}
