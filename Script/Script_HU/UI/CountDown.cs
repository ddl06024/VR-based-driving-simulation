using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountDown : MonoBehaviour
{
    public GameObject countdown;
    public AudioSource GetReady;
    public AudioSource GoAudio;
    //public GameObject LapTimer;
    public GameObject Car;
    private Controller carController;


    void Start()
    {
        Debug.Log("count down start");
        
        //LapTimer.SetActive(false);

        // Spawn한 Game Object 찾기
        Car = GameObject.FindWithTag("Player");

        Debug.Log($" find Car { Car }");
        carController = Car.GetComponent<Controller>();

        carController.wheelColliders[0].brakeTorque = 10000;
        carController.wheelColliders[1].brakeTorque = 10000;

        StartCoroutine(CountStart());
    }

    IEnumerator CountStart()
    {
        Managers.Sound.Play("Car/startup", Define.Sound.PlayStart);
        Managers.Sound.Play("Car/idle", Define.Sound.Idle);

        yield return new WaitForSeconds(0.5f);
        countdown.GetComponent<Text>().text = "5";
        Managers.Sound.Play("Car/readyup", Define.Sound.Ready, 0.5f);
        countdown.SetActive(true);
        yield return new WaitForSeconds(1);
        countdown.SetActive(false);
        countdown.GetComponent<Text>().text = "4";
        Managers.Sound.Play("Car/readyup", Define.Sound.Ready, 0.5f);
        countdown.SetActive(true);
        yield return new WaitForSeconds(1);
        countdown.SetActive(false);
        countdown.GetComponent<Text>().text = "3";
        Managers.Sound.Play("Car/readyup", Define.Sound.Ready, 0.5f);
        countdown.SetActive(true);
        yield return new WaitForSeconds(1);
        countdown.SetActive(false);
        countdown.GetComponent<Text>().text = "2";
        Managers.Sound.Play("Car/readyup", Define.Sound.Ready, 0.5f);
        countdown.SetActive(true);
        yield return new WaitForSeconds(1);
        countdown.SetActive(false);
        countdown.GetComponent<Text>().text = "1";
        Managers.Sound.Play("Car/readyup", Define.Sound.Ready, 0.5f);
        countdown.SetActive(true);
        yield return new WaitForSeconds(1);
        countdown.SetActive(false);

        //LapTimer.SetActive(true);
        carController.wheelColliders[0].brakeTorque = 0;
        carController.wheelColliders[1].brakeTorque = 0;

        // Play State & Background music start
        Managers.State.Set_State(Play_State.Start);
        Managers.Sound.Play("Car/Zephyr", Define.Sound.Background); // Idle 하고 Background 음악 두 가지 동시에 수정 요망!

    }

}