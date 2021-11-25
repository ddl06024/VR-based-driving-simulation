using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
   // public GameObject obj;
// public int num_car = 0;
    public List<GameObject> carlist = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        obj = other.gameObject;
        Debug.Log(other.name + "감지 시작!");

    }
    */

    public void OnTriggerEnter(Collider other)
    {

        carlist.Add(other.gameObject);
        //Debug.Log(other.name + "감지중!");

    }

    /*
    public void OnTriggerStay(Collider other)
    {
        carlist.Enqueue(other.gameObject);
        Debug.Log(other.name + "감지중!");

    }
    */

    public void OnTriggerExit(Collider other1)
    {

        carlist.Remove(other1.gameObject);
        //Debug.Log(other1.name + "감지 끝!");

    }
    
   



    // Update is called once per frame
    void Update()
    {
        //Debug.Log("차개수");
        //Debug.Log(carlist.Count);
        /*
        for (int i = 0; i < carlist.Count; i++) 
        {
            Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider>(), carlist..GetComponent<Collider>());
        }
         */   
    }
}
