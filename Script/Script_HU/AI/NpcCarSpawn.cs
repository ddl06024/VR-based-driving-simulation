using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC차량 스폰해준다. 
// 각 지역을 랜덤으로 선택하여, 그 지역에 각 한대의 차량을 스폰해준다. 

public class NpcCarSpawn : MonoBehaviour
{
    [SerializeField]
    private int NpcCar; // 스폰 차량 설정

    private int Waypoint_num;
    private List<Transform> duplicate_waypoint = new List<Transform>();


    private GameObject[] carPrefabs;
    //private GameObject[] startPoint;

    // startpoint 컴포넌트 찾기 (자식 object 찾기)
    /*private static GameObject FindChild(GameObject go, int index)                                                    
    { 
        Transform transform = go.transform.GetChild(index);
        return transform.gameObject;
        
    }*/

    private void Awake()
    {
        //startPoint = new GameObject[transform.childCount];
        carPrefabs = Resources.LoadAll<GameObject>("Cars_Pref");
        //Debug.Log(string.Format("carPrefabs {0}", carPrefabs.Length));
        /*for (int i = 0; i < transform.childCount; i++) 
        {
            startPoint[i] = FindChild(gameObject, i);
        }
        */
    }

    private void Start()
    {
        StartCoroutine(CarSpawn());
    }

 

    // 차 종류 선택
    private GameObject SelectCarPrefab() 
    {
        //int random_num = Random.Range(0, carPrefabs.Length);
        return carPrefabs[4];
    }

    // 스폰 장소 선택
    /*private GameObject SelectStartPoint()
    {
        int random_num = Random.Range(0, startPoint.Length);
        return startPoint[random_num];
    }
    */

    //스폰 장소에 선택된 차 스폰하기.
    IEnumerator CarSpawn()
    {
        int count = 0;
        while (count < NpcCar)
        {

            int random_num = Random.Range(0, carPrefabs.Length - 1);
            GameObject ped = carPrefabs[random_num];
            GameObject obj = Instantiate(ped);

            // 같은곳에서 하나의 차만 스폰되게 한다.          
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            while (duplicate_waypoint.Contains(child)) 
            {
                child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            }
            duplicate_waypoint.Add(child);
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypointer>();
            //왜인지 npccarcontroller가 활성화가 안되어서 따로 활성화 시켜줌.
            obj.GetComponent<NpcCarController>().enabled = true;
            obj.transform.position = child.position;
            obj.transform.Rotate(0, child.transform.eulerAngles.y + 180, 0);
            

            /*
            void OnTriggerEnter(Collider other)
            {
                if (other.gameObject.tag == "Car") 
                {
                    obj.transform.position = child.position - Vector3.forward * 70;
                    obj.transform.Rotate(0, child.transform.eulerAngles.y + 180, 0);
                }
            }
            */


          

            //Waypointer waypoint = child.GetComponent<Waypointer>();

            //duplicate_waypoint.Add(waypoint);
            
            yield return new WaitForEndOfFrame();
            count++;
            /*
            GameObject car = SelectCarPrefab();
            GameObject startpoint = SelectStartPoint();
            GameObject obj = Instantiate(car);

            obj.transform.position = startpoint.GetComponent<Transform>().position;
            obj.transform.rotation = startpoint.GetComponent<Transform>().rotation;

            yield return new WaitForEndOfFrame();
            count++;
            */

        }

    }


}


