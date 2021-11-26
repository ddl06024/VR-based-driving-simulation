using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParkScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Park_Day; // Data 전달 받으면 됨
    }

    private void Update()
    {
        // 게임 타이머가 종료하였거나, 실격시 다음 씬으로 
        // Managers.Scene.LoadScene(Define.Scene.Game,,,) 게임 종료 화면으로 

        if (Managers.State.Get_State() == Play_State.End)
        {
            Debug.Log("Game is The End");
            SceneManager.LoadScene(0);
        }
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear");
    }
}