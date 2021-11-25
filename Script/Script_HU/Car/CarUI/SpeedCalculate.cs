using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedCalculate : MonoBehaviour
{

    public Text speedui;
    public GameObject playercar;
    private Vector3 m_LastPosition;
    public float m_Speed;
    public float speed;


    void FixedUpdate()
    {
        m_Speed = GetSpeed();
        speed = m_Speed * 3.6f;
        speedui.GetComponent<Text>().text = string.Format("SPEED {0:00.00} km/h", m_Speed * 3.6f);

    }

    float GetSpeed()
    {
        float speed = (((playercar.transform.position - m_LastPosition).magnitude) / Time.deltaTime);
        m_LastPosition = playercar.transform.position;

        return speed;
    }
}
