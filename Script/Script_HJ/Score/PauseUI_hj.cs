using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI_hj : MonoBehaviour
{
    public Text UIPoint; // StagePoint 
    public GameObject pausePanel; // Game 중단시, 나타나는 Panel
    // pausePanel의 Btn
    public Button GoHomeBtn;
    public Button RetryBtn;
    public Button RestartBtn;
    // pause Btn
    public Button pauseBtn; // Game 중단 버튼 


    //SDKInputManager가 객체에 PauseUI_hj 함께 있어야함
    private SDKInputManager IM;
    private bool alreadyPressed = false;
    //public GameObject canvas;
    public GameObject pauseUI;

    private void Awake()
    {
        // Object & Btn assignment
        pausePanel = Util.FindChild(transform.gameObject, "pausePanel", true);

        GoHomeBtn = Util.FindChild<Button>(pausePanel, "goHomeBtn");
        RetryBtn = Util.FindChild<Button>(pausePanel, "retryBtn");
        RestartBtn = Util.FindChild<Button>(pausePanel, "restartBtn");
        pauseBtn = Util.FindChild<Button>(GameObject.Find("pauseUI"), "pauseBtn", true);
        UIPoint = Util.FindChild<Text>(GameObject.Find("StagePoint"), "TitleLabel");
        
        Debug.Log($"{pausePanel != null}, {GoHomeBtn != null}, {pausePanel != null}, {RestartBtn != null}, {pauseBtn != null}, {UIPoint != null}");

        // AddListener 
        GoHomeBtn.onClick.AddListener(() => {
            // 나중에 채린이 Scene으로 바꿔야함
            Debug.Log("Home Button Clicked");
        });

        RetryBtn.onClick.AddListener(Retry);
        RestartBtn.onClick.AddListener(Restart);
        pauseBtn.onClick.AddListener(Pause);

    }
    void Start()
    {
        pausePanel.SetActive(false);
        UIPoint.text = "Score: " + Managers.Score.GetScore().ToString();

        IM = GetComponent<SDKInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UIPoint.text = "Score: " + Managers.Score.GetScore().ToString();

        if (Managers.State.Get_State() == Play_State.GameOver)
        {
            int[] DeductInfo = Managers.Score.GetDeductInfo();

            // 주행 실패했을때
            if (Managers.Score.GetScoreOut() != Define.ScoreOut.Clear)
            {
                UIPoint.text = "FAIL";
            }
        }

        if (IM.SInput == true)
        {
            //print($"인풋값{IM.TInput}");
            IsPressedTbutton();
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("pause");
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void IsPressedTbutton()
    {
        if (alreadyPressed == false)
        {
            Time.timeScale = 0;
            //pausePanel.SetActive(true);
            print("켜지기");
            pausePanel.SetActive(true);
            pauseUI.SetActive(false);


        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            pauseUI.SetActive(true);
        }
        alreadyPressed = !alreadyPressed;

    }
}
