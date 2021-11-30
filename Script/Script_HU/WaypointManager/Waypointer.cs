using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypointer : MonoBehaviour
{
    public Waypointer previousWaypoint;
    public Waypointer nextWaypoint;

    //public bool shouldBranch;
    //public bool shouldBranch1;

    //left: 1, right: 2, straigt = ''
    public int left_or_right;

    [Range(0f, 5f)]
    public float width = 1.5f;

    public List<Waypointer> branches = new List<Waypointer>();

    [Range(0f, 5f)]
    public float branchRatio = 0.5f;

    public List<Waypointer> branches1 = new List<Waypointer>();

    [Range(0f, 5f)]
    public float branchRatio1 = 0.5f;




    // return a random point based on the waypoint width and basically  we will give some degree of freedom for characters
    // to move between when they are moving towards a waypoint 
    public Vector3 GetPosition() 
    {
        /*
         Vector3 minBound = transform.position + transform.right * width / 2f;
         Vector3 maxBound = transform.position - transform.right * width / 2f;
         return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
         */
        Vector3 minBound = transform.position + transform.right;
        Vector3 maxBound = transform.position - transform.right;
        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }

  
}



