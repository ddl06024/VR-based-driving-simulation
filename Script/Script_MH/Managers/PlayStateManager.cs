using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Play State
public enum Play_State
{
    Ready,
    Start,
    Playing,
    GameOver,
    End,
}
public class PlayStateManager
{
    private Play_State state;

    public void Init()
    {
        GameObject root = GameObject.Find("@PlayState");

        if (root == null)
        {
            root = new GameObject { name = "@PlayState" };
            Object.DontDestroyOnLoad(root);
            root.AddComponent<PlayState>();
        }
    }
    // 현재 상태 바꿈
    public void Set_State(Play_State state)
    {
        this.state = state;
    }

    public Play_State Get_State()
    {
        return this.state;
    }

    public void Clear_state()
    {
        this.state = Play_State.Ready;
    }

}