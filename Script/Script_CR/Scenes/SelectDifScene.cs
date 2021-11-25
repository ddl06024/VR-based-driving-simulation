using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.SelectDif;
    }
    public override void Clear()
    {
        Debug.Log("SelectDifScene Clear");
    }
}
