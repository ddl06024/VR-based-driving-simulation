using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTest_hj : MonoBehaviour
{
    public int stagePoint = 100;
    public PlayerTest_hj playcar;

    //한번충돌시 한번 점수 감점
    private bool onetimededuction = false;

    //시간
    private string time;
    private string pasttime;

    //과속
    public SpeedCalculate speedCalculate;

    //급정거
    //드래그드랍해줘야함
    private SDKInputManager IM;
    private float lastbrakeInput;


    // Collider 컴포넌트의 is Trigger가 false인 상태로 충돌을 시작했을 때
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        
        if (other.gameObject.CompareTag("Animals") && (pasttime != time || pasttime == null))
        {
            //Debug.Log("동물 충돌");
            if (!onetimededuction )
            {
                //scoretest_hj.stagePoint -= 5;
                stagePoint -= 7;

                onetimededuction = !onetimededuction;
                pasttime = time;
            }

            //Debug.Log("Score: " + stagePoint);
            //UIPoint.text = "Score: " + Managers.Score.GetScore();

            if (stagePoint <= 60)
            {
                //Debug.Log("실격");
                playcar.OnDie();

                //UIPoint.text = "Score: " + stagePoint.ToString();
                //FinishMent.text = "실격입니다. 다시 시도해보세요.";
                //DeductReason.text = "점수 미달입니다.";
                //TotalPoints.text = stagePoint.ToString();
                //scorePanel.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Car"))
        {
            //Debug.Log("차량 충돌");
            stagePoint = 0;
            if (stagePoint <= 0)
            {
                playcar.OnDie();

                //UIPoint.text = "Score: 0";
                //scorePanel.SetActive(true);
                //FinishMent.text = "실격입니다. 다시 시도해보세요.";
                //DeductReason.text = "반대방향 주행 차량과 충돌함.";
                //DeductPoints.text = "Fail";
                //TotalPoints.text = stagePoint.ToString();
            }
        }
        else if (other.gameObject.CompareTag("Cliff"))
        {
            stagePoint = 0;
            if (stagePoint <= 0)
            {
                //Debug.Log("절벽 충돌");
                playcar.OnDie();

                //UIPoint.text = "Score: 0";
                //FinishMent.text = "실격입니다. 다시 시도해보세요.";
                //DeductReason.text = "절벽과 충돌함.";
                //DeductPoints.text = "Fail";
                //TotalPoints.text = stagePoint.ToString();
                //scorePanel.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            playcar.OnDie();
            //TotalPoints.text = stagePoint.ToString();
            Debug.Log("Finish");
            //scorePanel.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (other.gameObject.CompareTag("Animals"))
        {
            onetimededuction = !onetimededuction;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        IM = playcar.GetComponent<SDKInputManager>();
        //pausePanel.SetActive(false);
        //scorePanel.SetActive(false);

        //UIPoint.text = "Score: " + stagePoint.ToString();
    }

    private void Update()
    {
        time = System.DateTime.Now.ToString();
        deduction_for_speeding();
        suddenstop();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        //pausePanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        //pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    // 과속 채점
    private void deduction_for_speeding()
    {
        if (speedCalculate.speed >= 20)
        {
            if (!onetimededuction)
            {
                //scoretest_hj.stagePoint -= 5;
                stagePoint -= 5;
                Debug.Log($"자동차 스피드 {speedCalculate.speed} 신호위반감점");
                onetimededuction = !onetimededuction;
            }

        }
        else if (speedCalculate.speed < 20)
        {
            if (onetimededuction)
            {
                onetimededuction = !onetimededuction;
            }
        }
    }

    public void suddenstop()
    {
        // 브레이크 안밟으면 -1 ~ 밟으면 1
        float degree_brake = ((IM.brake - lastbrakeInput) / Time.deltaTime);
        lastbrakeInput = IM.brake;
        if (degree_brake >= 15)
        {
            if (!onetimededuction)
            {
                //scoretest_hj.stagePoint -= 5;
                stagePoint -= 10;
                print("브레이크점수감점");
                onetimededuction = !onetimededuction;
            }

        }
        else if (degree_brake < 15)
        {
            if (onetimededuction)
            {
                onetimededuction = !onetimededuction;
            }
        }
    }
}
