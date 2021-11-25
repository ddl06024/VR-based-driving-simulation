using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignUpScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.SignUp;
    }

    private void Update()
    {

    }
    public override void Clear()
    {
        Debug.Log("SignUpScene Clear");
    }
}
