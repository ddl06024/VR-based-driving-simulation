using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        SignUp,
        Menu,
        Game,
        MyPage,
        SelectMap,
        SelectDif,
        Mountain_Level_1_Day,
        Mountain_Level_1_Night,
        Mountain_Level_2_Day,
        Mountain_Level_2_Night,
        Mountain_Level_3_Day,
        Mountain_Level_3_Night,
        Normal_Level_1_Day,
        Normal_Level_1_Night,
        Normal_Level_2_Day,
        Normal_Level_2_Night,
        Normal_Level_3_Day,
        Normal_Level_3_Night,
        School_Level_1_Day,
        School_Level_1_Night,
        School_Level_2_Day,
        School_Level_2_Night,
        School_Level_3_Day,
        School_Level_3_Night,
        Park_Level_1_Day,
        Park_Level_1_Night,
        Park_Level_2_Day,
        Park_Level_2_Night,
        Park_Level_3_Day,
        Park_Level_3_Night,

    }
    public enum UIEvent
    {
        Click,

    }

    public enum ScoreDeduct
    {
        StartMiss, // 출발
        AnimalCollision,
        CarCollision,
        ClifCollision,
        Speeding,
        SuddenStop,
        LineCollision,
        BuildingCollision,
        PedestrainsCollision,
        TrafficsignViolation,
        MaxCount,


    }

    public enum ScoreOut
    {
        TimerOut,
        Threshold,
        Collision,
        Clear,
    }

    public enum Sound
    {
        UI,
        Ready,
        PlayStart,
        Idle,
        SwitchGear,
        Background,
        LowEngine,
        HighEngine,
        MaxCount, // sound 개수를 위해
    }

    public Dictionary<string, int> Deduction_List = new Dictionary<string, int>()
    {
        {"Don't Signal", 5 }, //방향지시등 X
        {"Stop Short", 10}, //급정거
        {"Abnormal Speed", 5 }, //적정 속도 X
        {"Collide with Human or Car", -1}, //사람 또는 차량과 충돌
        {"Traffic Violation", 7}, //신호 위반
        {"Collide with Animal", 7}, //동물과 충돌
        {"Collide with Car in Mounatin", -1}, //반대편 주행 차량과 충돌
        {"Collide with Cliff", -1 }, //절벽과 충돌
        
    };

}
