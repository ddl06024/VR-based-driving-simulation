using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindLights : MonoBehaviour
{

    public GameObject[] Spot_lights = new GameObject[93];
    public Light[] spot_lights = new Light[93];
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 93; i++) 
        {
            //GameObject.Find("찾고자 하는 오브젝 이름");
            //SpotLight1 (0)
            Spot_lights[i] = GameObject.Find(string.Format("SpotLight1 ({0})", i));
            Debug.Log(string.Format("SpotLight1 ({0})", i));
            spot_lights[i] = Spot_lights[i].GetComponent<Light>();
            spot_lights[i].range = 20f;
            spot_lights[i].spotAngle = 50f;
            spot_lights[i].intensity = 12f;
        }

    }
    /*
    public void SetSpotLights()
    {
        for (int i = 0; i < 93; i++)
        {
            //Debug.Log(string.Format("{0} 스팟", i));
            spot_lights[i].range = 100f;
            spot_lights[i].spotAngle = 50f;
            spot_lights[i].intensity = 12f;
            //Debug.Log(string.Format("{0} spitangle", spot_lights[i].spotAngle));
        }
    }
    */


}
