using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayState: MonoBehaviour
{
    private Play_State current_state;
    public UnityEvent onPlayReady;
    public UnityEvent onPlayStart;
    public UnityEvent onPlaying;
    public UnityEvent onPlayEnd;

    private void End()
    {
        Debug.Log("Play Stop");

        onPlayEnd.Invoke();

    }
    void Start()
    {
        // 시작 할 때의  
        Managers.State.Set_State(Play_State.Ready);
    }

    void Update()
    {
        current_state = Managers.State.Get_State();

        Debug.Log($"현재 상태: { current_state }");

        switch (current_state)
        {
            case Play_State.Ready:
                // countdown 시작 
                // 자동차 멈춤
                // 자동차 감점 감지
                break;

            case Play_State.Start:
                Debug.Log("Play Start");
                onPlayStart.Invoke();
                Managers.State.Set_State(Play_State.Playing);
                break;

            case Play_State.Playing:
                
                break;

        }


    }
}
