using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button_SelectDif : UI_Popup
{
    public Slider DifSlider;
    public TMP_Text text;
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

        Bind<Button>(typeof(Buttons)); //enum 이름은 Buttons이고 Button의 컴포넌트를 갖는 객체를 찾을 것
        //GetButton((int)Buttons.LoginButton).gameObject.BindEvent(OnButtonClicked);
        BindEvent(GetButton((int)Buttons.NextButton).gameObject, OnNextButtonClicked);
        BindEvent(DifSlider.gameObject, OnValueChanged);
    }

    public int Find_Map_Name()
    {

        string map_name = user_info.User_Info.Get_game_map();
        int map_diff = user_info.User_Info.Get_game_diff();
        bool d_n = user_info.User_Info.Get_day_or_night();
        int start = 0;
        if (map_name == "Mountain")
        {
            start = 8;
        }
        else if (map_name == "Normal")
        {
            start = 14;
        }
        else if (map_name == "School")
        {
            start = 20;
        }
        else if (map_name == "Park")
        {
            start = 26;
        }

        if (map_diff == 1 && map_name != "Park")
        {
            start = start + 2;
        }
        else if (map_diff == 2 && map_name != "Park")
        {
            start = start + 4;
        }

        if (d_n == false)
        {
            start = start + 1;
        }

        return start;
    }

    public void OnNextButtonClicked(PointerEventData data)
    {
        //선택한 난이도 정보 전달하는 기능 추가
        user_info.User_Info.Set_game_diff((int)DifSlider.value);



        if (Database.db.Set_Diff())
        {
            int selected_map = Find_Map_Name();
            Managers.Scene.LoadScene((Define.Scene)Enum.ToObject(typeof(Define.Scene), selected_map));
        }
        else
        {
            Debug.Log("Failed to Diff Select");
        }
    }

    public void ChangeText(TMP_Text text, string s)
    {
        text.text = s;
    }

    public void OnValueChanged(PointerEventData data)
    {
        float difvalue = DifSlider.value;
        switch (difvalue)
        {
            case 0:
                ChangeText(text, "Easy");
                break;
            case 1:
                ChangeText(text, "Normal");
                break;
            case 2:
                ChangeText(text, "Hard");
                break;

        }
    }
}
