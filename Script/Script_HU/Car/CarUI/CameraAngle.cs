using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    public Transform Car;

    private float anglex;
    private float angley;
    private float anglez;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anglex = Car.eulerAngles.x;
        angley = Car.eulerAngles.y;
        anglez = Car.eulerAngles.z;

        transform.eulerAngles = new Vector3(0, angley, 0);
    }
}
