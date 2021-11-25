using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Menu;
    }

    private void Update()
    {
        
    }
    public override void Clear()
    {
        Debug.Log("MenuScene Clear");
    }
}
