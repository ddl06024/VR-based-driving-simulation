using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPageScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.MyPage;
    }

    private void Update()
    {

    }
    public override void Clear()
    {
        Debug.Log("MyPageScene Clear");
    }
}
