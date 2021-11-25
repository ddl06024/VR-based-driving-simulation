using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text ClockText;
    float time;
    bool TimerisRunning = false;
    public float timeRemaining = 10;

    public void OnPlayStart()
    {
        TimerisRunning = true;
    }

    private void Awake()
    {
        TimerisRunning = false;
        // Text assignment
        ClockText = Util.FindChild<Text>(GameObject.Find("TimerUI"), "TimeText", true);
        GameObject.Find("@PlayState").GetComponent<PlayState>().onPlayStart.AddListener(OnPlayStart);
    }
    void Start()
    {
        // 한 Frame 기다리기
        StartCoroutine(WaitFrame());
    }

    IEnumerator WaitFrame()
    {
        yield return new WaitForSeconds(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerisRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisPlayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time is over!!");
                Managers.Score.ScoreOut(Define.ScoreOut.TimerOut);
                TimerisRunning = false;
            }
        }
    }

    void DisPlayTime(float time)
    {
        time += 1;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        ClockText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}