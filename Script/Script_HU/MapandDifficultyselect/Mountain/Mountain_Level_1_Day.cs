﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mountain_Level_1_Day : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Mountain_Level_1_Day;
    }
    private void Update()
    {
        if (Managers.State.Get_State() == Play_State.End)
        {
            Managers.Scene.LoadScene(Define.Scene.Menu);
        }
    }
    public override void Clear()
    {
    }
}
