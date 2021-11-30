using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_Level_3_Day : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Park_Level_3_Day;
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
