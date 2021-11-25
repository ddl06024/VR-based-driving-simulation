using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubestartTrigger : MonoBehaviour
{
    public GameObject obj;
    public NpcCarController npcCarController;
    public int flag = 0;

    public void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        obj = other.gameObject;
        npcCarController = obj.GetComponent<NpcCarController>();
        //npcCarController.movementSpeed = 0;
        flag = 1;
    }
    
    public void OnTriggerExit(Collider other)
    {
        obj = null;
        //npcCarController = obj.GetComponent<NpcCarController>();
        //npcCarController.movementSpeed = 0;
        flag = 0;
    }
    

}
