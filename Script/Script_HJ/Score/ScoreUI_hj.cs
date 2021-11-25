using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreUI_hj : MonoBehaviour
{
    public GameObject scorePanel;
    public GameObject SpeedTimePanel;
    public GameObject PointPanel;

    //public List<TextMesh> GameReport = new List<TextMesh>(); 

    public Text FinishMent;

    // Detail Score UI
    //public Text DeductReason = null;
    //public TextMeshProUGUI DeductPoints = null;
    //public TextMeshProUGUI DeductNum = null;
    //public Text DeductReason1;
    //public Text DeductReason2;
    //public Text DeductReason3;
    //public Text DeductReason4;
    //public Text DeductReason5;

    //public TextMeshProUGUI DeductPoints1;
    //public TextMeshProUGUI DeductPoints2;
    //public TextMeshProUGUI DeductPoints3;
    //public TextMeshProUGUI DeductPoints4;
    //public TextMeshProUGUI DeductPoints5;

    public TextMeshProUGUI DeductNum1;
    public TextMeshProUGUI DeductNum2;
    public TextMeshProUGUI DeductNum3;
    public TextMeshProUGUI DeductNum4;
    public TextMeshProUGUI DeductNum5;
    public TextMeshProUGUI DeductNum6;

    public TextMeshProUGUI TotalPoints = null;

    // Exit Btn
    public Button ExitBtn;

    // Scroll View
    //private ScrollRect scroll_rect = null;

    private bool isEnd = false;

    private void Awake()
    {
        Debug.Log("ScoreUI awake");

        // assignment 
        scorePanel = Util.FindChild(transform.gameObject, "scorePanel", true);
        SpeedTimePanel = Util.FindChild(GameObject.Find("UI"), "Speed&TimerUI", true);
        PointPanel = GameObject.Find("stageUI");
        scorePanel.SetActive(false);

        FinishMent = Util.FindChild<Text>(scorePanel, "FinishMent", true);
        //GameReport = Util.FindChildTypeof<TextMesh>(GameObject.Find("GameReport1"));
        //scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
        TotalPoints = Util.FindChild<TextMeshProUGUI>(scorePanel, "Total", true);

        //DeductReason1 = Util.FindChild<Text>(scorePanel, "Reason1", true);
        //DeductPoints1 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry1", true), "Points1", true);
        DeductNum1 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry1", true), "Number1", true);

        //DeductReason2 = Util.FindChild<Text>(scorePanel, "Reason2", true);
        //DeductPoints2 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry2", true), "Points2", true);
        DeductNum2 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry2", true), "Number2", true);

        //DeductReason3 = Util.FindChild<Text>(scorePanel, "Reason3", true);
        //DeductPoints3 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry3", true), "Points3", true);
        DeductNum3 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry3", true), "Number3", true);

        //DeductReason4 = Util.FindChild<Text>(scorePanel, "Reason4", true);
        //DeductPoints4 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry4", true), "Points4", true);
        DeductNum4 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry4", true), "Number4", true);

        //DeductReason5 = Util.FindChild<Text>(scorePanel, "Reason5", true);
        //DeductPoints5 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry5", true), "Points5", true);
        DeductNum5 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry5", true), "Number5", true);

        DeductNum6 = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry6", true), "Number6", true);

        //DeductReason = Util.FindChild<Text>(scorePanel, "Reason1", true);
        //DeductPoints = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry1", true), "Points1", true);
        //DeductNum = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry1", true), "Number1", true);

        //ExitBtn = Util.FindChild<Button>(scorePanel, "ExitButton", true);
        //ExitBtn.onClick.AddListener(GameTheEnd);

        // Debug
        //Debug.Log($"{FinishMent != null}, {TotalPoints != null}, {DeductPoints != null}");
        //Debug.Log($"{DeductReason != null}, {ExitBtn != null}");


    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("state");
    }

    void Update()
    {
        // Game State가 Game Over -> 게임 종료
        if (Managers.State.Get_State() == Play_State.GameOver)
        {
            //Time.timeScale = 0;
            int[] DeductInfo = Managers.Score.GetDeductInfo();
            //scroll_rect.verticalNormalizedPosition = 0.0f;

            //scorePanel.SetActive(true);
            //SpeedTimePanel.SetActive(false);
            //PointPanel.SetActive(false);
            //Invoke("StopPanel", 3f);

            // 주행 성공했을때
            if (Managers.Score.GetScoreOut() == Define.ScoreOut.Clear)
            {
                //scorePanel.SetActive(true);
                FinishMent.text = "주행 성공";
                //DeductReason.text = Managers.Score.GetScoreOut().ToString();
                TotalPoints.text = Managers.Score.GetScore().ToString();
                if (DeductInfo[(int)Define.ScoreDeduct.StartMiss] >= 0)
                {
                    //DeductReason1.text = "Start Miss";
                    //DeductPoints1.text = "-10";
                    DeductNum1.text = DeductInfo[(int)Define.ScoreDeduct.StartMiss].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.AnimalCollision] >= 0)
                {
                    //DeductReason2.text = "Amimal Collision";
                    //DeductPoints2.text = "-7";
                    DeductNum2.text = DeductInfo[(int)Define.ScoreDeduct.AnimalCollision].ToString();
                    //DeductReason.text += "\n" + Managers.Score.GetScoreOut().ToString();
                    //DeductPoints.text += "\n" + "-7";
                    //DeductNum.text += "\n" + DeductInfo[1];
                    //scroll_rect.verticalNormalizedPosition = 0.0f;
                    //TotalPoints.text = Managers.Score.GetScore().ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.Speeding] >= 0)
                {
                    //DeductReason3.text = "Speeding";
                    //DeductPoints3.text = "-5";
                    DeductNum3.text = DeductInfo[(int)Define.ScoreDeduct.Speeding].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.SuddenStop] >= 0)
                {
                    //DeductReason4.text = "Sudden Stop";
                    //DeductPoints4.text = "-10";
                    DeductNum4.text = DeductInfo[(int)Define.ScoreDeduct.SuddenStop].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.BuildingCollision] >= 0)
                {
                    //DeductReason5.text = "Building Collision";
                    //DeductPoints5.text = "-5";
                    DeductNum5.text = DeductInfo[(int)Define.ScoreDeduct.BuildingCollision].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation] >= 0)
                {
                    DeductNum6.text = DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation].ToString();
                }
            }
            else if (Managers.Score.GetScoreOut() == Define.ScoreOut.TimerOut)
            {
                //scorePanel.SetActive(true);
                FinishMent.text = "시간 초과되어 주행 실패";
                //DeductReason.text = Managers.Score.GetScoreOut().ToString();
                TotalPoints.text = Managers.Score.GetScore().ToString();
                if (DeductInfo[(int)Define.ScoreDeduct.StartMiss] >= 0)
                {
                    DeductNum1.text = DeductInfo[(int)Define.ScoreDeduct.StartMiss].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.AnimalCollision] >= 0)
                {
                    DeductNum2.text = DeductInfo[(int)Define.ScoreDeduct.AnimalCollision].ToString();
                    //DeductReason.text += "\n" + Managers.Score.GetScoreOut().ToString();
                    //DeductPoints.text += "\n" + "-7";
                    //DeductNum.text += "\n" + DeductInfo[1];
                    //scroll_rect.verticalNormalizedPosition = 0.0f;
                    //TotalPoints.text = Managers.Score.GetScore().ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.Speeding] >= 0)
                {
                    DeductNum3.text = DeductInfo[(int)Define.ScoreDeduct.Speeding].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.SuddenStop] >= 0)
                {
                    DeductNum4.text = DeductInfo[(int)Define.ScoreDeduct.SuddenStop].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.BuildingCollision] >= 0)
                {
                    DeductNum5.text = DeductInfo[(int)Define.ScoreDeduct.BuildingCollision].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation] >= 0)
                {
                    DeductNum6.text = DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation].ToString();
                }
            }
            else if (Managers.Score.GetScoreOut() == Define.ScoreOut.Threshold)
            {
                //scorePanel.SetActive(true);
                FinishMent.text = "점수 미달되어 주행 실패!";
                //DeductReason.text = Managers.Score.GetScoreOut().ToString();
                TotalPoints.text = Managers.Score.GetScore().ToString();
                if (DeductInfo[(int)Define.ScoreDeduct.StartMiss] >= 0)
                {
                    DeductNum1.text = DeductInfo[(int)Define.ScoreDeduct.StartMiss].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.AnimalCollision] >= 0)
                {
                    DeductNum2.text = DeductInfo[(int)Define.ScoreDeduct.AnimalCollision].ToString();
                    //DeductReason.text += "\n" + Managers.Score.GetScoreOut().ToString();
                    //DeductPoints.text += "\n" + "-7";
                    //DeductNum.text += "\n" + DeductInfo[1];
                    //scroll_rect.verticalNormalizedPosition = 0.0f;
                    //TotalPoints.text = Managers.Score.GetScore().ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.Speeding] >= 0)
                {
                    DeductNum3.text = DeductInfo[(int)Define.ScoreDeduct.Speeding].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.SuddenStop] >= 0)
                {
                    DeductNum4.text = DeductInfo[(int)Define.ScoreDeduct.SuddenStop].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.BuildingCollision] >= 0)
                {
                    DeductNum5.text = DeductInfo[(int)Define.ScoreDeduct.BuildingCollision].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation] >= 0)
                {
                    DeductNum6.text = DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation].ToString();
                }
            }
            else
            {
                //DeductReason.text = Managers.Score.GetScoreOut().ToString();
                //TotalPoints.text = "fail";
                if (DeductInfo[(int)Define.ScoreDeduct.StartMiss] >= 0)
                {
                    DeductNum1.text = DeductInfo[(int)Define.ScoreDeduct.StartMiss].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.AnimalCollision] >= 0)
                {
                    DeductNum2.text = DeductInfo[(int) Define.ScoreDeduct.AnimalCollision].ToString();
                    //DeductReason.text += "\n" + Managers.Score.GetScoreOut().ToString();
                    //DeductPoints.text += "\n" + "-7";
                    //DeductNum.text += "\n" + DeductInfo[1];
                    //scroll_rect.verticalNormalizedPosition = 0.0f;
                    //TotalPoints.text = Managers.Score.GetScore().ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.CarCollision] != 0)
                {
                    TotalPoints.text = "FAIL";
                    FinishMent.text = "차량 충돌하여 주행 실패!";
                }
                if (DeductInfo[(int)Define.ScoreDeduct.ClifCollision] != 0)
                {
                    TotalPoints.text = "FAIL";
                    FinishMent.text = "절벽 충돌하여 주행 실패!";
                }
                if (DeductInfo[(int)Define.ScoreDeduct.Speeding] >= 0)
                {
                    DeductNum3.text = DeductInfo[(int)Define.ScoreDeduct.Speeding].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.SuddenStop] >= 0)
                {
                    DeductNum4.text = DeductInfo[(int)Define.ScoreDeduct.SuddenStop].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.PedestrainsCollision] != 0)
                {
                    TotalPoints.text = "FAIL";
                    FinishMent.text = "절벽 충돌하여 주행 실패!";
                }
                if (DeductInfo[(int)Define.ScoreDeduct.BuildingCollision] >= 0)
                {
                    DeductNum5.text = DeductInfo[(int)Define.ScoreDeduct.BuildingCollision].ToString();
                }
                if (DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation] >= 0)
                {
                    DeductNum6.text = DeductInfo[(int)Define.ScoreDeduct.TrafficsignViolation].ToString();
                }
            }
            
            scorePanel.SetActive(true);
            SpeedTimePanel.SetActive(false);
            PointPanel.SetActive(false); 

            if (!isEnd)
            {
                StartCoroutine(StopPanel());
                isEnd = !isEnd;
            }

        }
    }
    public void GameTheEnd()
    {
        Managers.State.Set_State(Play_State.End);
    }
    
    IEnumerator StopPanel()
    {
        yield return new WaitForSeconds(7.0f);
        Debug.Log("game coroutine start");
        Debug.Log("Game End");
        scorePanel.SetActive(false);
        Managers.State.Set_State(Play_State.End);
        Time.timeScale = 0;
    }
}