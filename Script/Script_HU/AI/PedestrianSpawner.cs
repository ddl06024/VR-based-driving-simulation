using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSpawner : MonoBehaviour
{
    [SerializeField]
    public int NpcPed = 8; // 스폰 보행자 설정

    public GameObject[] pedestrianPrefabs;

    private void Awake()
    {

        pedestrianPrefabs = Resources.LoadAll<GameObject>("Pedestrains_Pref");
       
    }

    void Start()
    {
        StartCoroutine(PedestrianSpawn());
    }

    public GameObject SelectPedestrianPrefab()
    {
        int random_num = Random.Range(0, pedestrianPrefabs.Length);
        return pedestrianPrefabs[random_num];
    }


    IEnumerator PedestrianSpawn()
    {
        int count = 0;
 
        while (count < NpcPed)
        {
            int random_num = Random.Range(0, pedestrianPrefabs.Length -1);            
            GameObject ped = pedestrianPrefabs[random_num];
            GameObject obj = Instantiate(ped);

            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypointer>();
            obj.transform.position = child.position;


            yield return new WaitForEndOfFrame();
            count++;


        }

        
        

    }

}
