using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMapScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.SelectMap;
    }

    private void Update()
    {

    }
    public override void Clear()
    {
        Debug.Log("SelectMapScene Clear");
    }
}
