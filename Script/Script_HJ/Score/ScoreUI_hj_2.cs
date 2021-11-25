using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Play State가 GameOver 후에 최종 결과 RePort
/// Map, Difficulty, S/F 결과 => GameReport
/// Resons of Deduction => DetailScore/ Contents/entry1
/// 
/// => Score UI 아마도 보충 요망
/// </summary>
/// 
public class ScoreUI_hj_2 : MonoBehaviour
{
    public GameObject scorePanel;
    
    //public List<TextMesh> GameReport = new List<TextMesh>(); 
    
    public Text FinishMent;

    // Detail Score UI
    public Text DeductReason;
    public TextMeshProUGUI DeductPoints;
    public TextMeshProUGUI TotalPoints;

    // Exit Btn
    public Button ExitBtn;

    private void Awake()
    {
        Debug.Log("ScoreUI awake");
        
        // assignment 
        scorePanel = Util.FindChild(transform.gameObject, "scorePanel", true);
        scorePanel.SetActive(false);
        FinishMent = Util.FindChild<Text>(scorePanel, "FinishMent", true);
        //GameReport = Util.FindChildTypeof<TextMesh>(GameObject.Find("GameReport1"));
        TotalPoints = Util.FindChild<TextMeshProUGUI>(scorePanel, "Total", true);
        DeductReason = Util.FindChild<Text>(scorePanel, "Reason", true);
        DeductPoints = Util.FindChild<TextMeshProUGUI>(Util.FindChild(scorePanel, "entry1", true), "Points", true);

        ExitBtn = Util.FindChild<Button>(scorePanel, "ExitButton", true);
        ExitBtn.onClick.AddListener(GameTheEnd);
        
        // Debug
        Debug.Log($"{FinishMent != null}, {TotalPoints != null}, {DeductPoints != null}");
        Debug.Log($"{DeductReason != null}, {ExitBtn != null}");
    
    }
    void Start()
    {
        Debug.Log("state");
    }

    // GUI 변경 Update 안됨 
    void Update()
    {
        // Game State가 Game Over -> 게임 종료
        if(Managers.State.Get_State() == Play_State.GameOver)
        {
            int[] DeductInfo = Managers.Score.GetDeductInfo();

            // 주행의 성공했을때
            if (Managers.Score.GetScoreOut() == Define.ScoreOut.Clear)
            {
                FinishMent.text = "주행 성공";
                DeductReason.text = "동물과 부딪힘";
                DeductPoints.text = DeductInfo[(int)Define.ScoreDeduct.AnimalCollision].ToString();
                TotalPoints.text = Managers.Score.GetScore().ToString();
                scorePanel.SetActive(true);
            }
            else
            {
                FinishMent.text = "주행 실패";
                DeductReason.text = Managers.Score.GetScoreOut().ToString();
                TotalPoints.text = Managers.Score.GetScore().ToString();
                scorePanel.SetActive(true);
            }
                
  
        }
    }
    public void GameTheEnd()
    {
        Managers.State.Set_State(Play_State.End);
    }
}
