using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Days : MonoBehaviour
{
    

    public GameObject Day;
    public GameObject Night;
    public GameObject Main_light;
    private Light main_light;
    //public GameObject[] Spot_lights;
    private Light[] spot_lights = new Light[20];

    public GameObject findlightsobject;
    private FindLights findlights;
    public Material SkyboxNight;
    public Material SkyboxDay;
    public Color FogColorNight;
    public Color FogColorDay;

    public float Intensity;


    void Start()
    {
        //main_light = Main_light.GetComponent<Light>();
        //findlights = findlightsobject.GetComponent<FindLights>();
        //Debug.Log(string.Format("라이트개수: {0}", findlights.Spot_lights.Length));
        // 낮
        RenderSettings.skybox = SkyboxDay;
        RenderSettings.fogColor = FogColorDay;
        Night.SetActive(false);
        Day.SetActive(true);
        main_light.intensity = 1f;

    }

    public void SetSpotLights() 
    {
        for (int i = 0; i < findlights.Spot_lights.Length; i++) 
        {
            //Debug.Log(string.Format("{0} 스팟", i));
            findlights.spot_lights[i].range = 15f;
            findlights.spot_lights[i].spotAngle = 50f;
            findlights.spot_lights[i].intensity = 12f;
            //Debug.Log(string.Format("{0} spitangle", spot_lights[i].spotAngle));
        }
    }
    /*
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            if (Day.activeSelf)
            {
                // 밤
                RenderSettings.skybox = SkyboxNight;
                RenderSettings.fogColor = FogColorNight;
                Night.SetActive(true);
                Day.SetActive(false);
                main_light.intensity = 0.5f;
                SetSpotLights();
            }

            else
            {
                // 낮
                RenderSettings.skybox = SkyboxDay;
                RenderSettings.fogColor = FogColorDay;
                Night.SetActive(false);
                Day.SetActive(true);
                main_light.intensity = 1f;
            }
        }

        
    }
    */




   
}
