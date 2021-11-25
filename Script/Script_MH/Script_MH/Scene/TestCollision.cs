using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private int count = 0;
    public WheelCollider[] LFC;
    WheelHit hit;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"This Collision! {collision.gameObject.tag}");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        count++;
        Debug.Log($"This Trigger{other.gameObject.tag} & {count}");
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LFC[0].GetGroundHit(out hit))
        {
            Debug.Log($"is hit {hit.collider}");
        }
    }
}
