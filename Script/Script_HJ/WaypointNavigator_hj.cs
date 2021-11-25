using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaypointNavigator_hj : MonoBehaviour
{
    public CharacteNavigationController_hj controller_pedestrians;
    public NpcCarController_hj controller_cars;
    public Waypointer_hj currentWaypoint;

    private int direction;

    public bool shouldBranch1;

    private void Awake()
    {
        //controller = GetComponent<CharacteNavigationController>();
        if (gameObject.tag == "Car")
        {
            controller_cars = GetComponent<NpcCarController_hj>();
            //Debug.Log("Car");
        }
        else if (gameObject.tag == "Animals")
        {
            controller_pedestrians = GetComponent<CharacteNavigationController_hj>();
           // Debug.Log("Animal");
        }
        else
        {
            //Debug.Log("tag오류");
        }

 
    }


    void Start()
    {
        //지정된 currentWayposition을 destination이라고 설정한다.
        // 자동차컨트롤러이면 
        if (controller_cars != null)
        {
            controller_cars.SetDestination(currentWaypoint.GetPosition());

        }
        else 
        {
            controller_pedestrians.SetDestination(currentWaypoint.GetPosition());
            //direction을 통해 0이나 1값을 받는다. 
            direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        }
    }

    //direction = 0 이면, 다음 waypoint를 destionation으로설정하고
    //1이면, 이전 waypoint를 destination으로 설정한다. 
    void Update()
    {
        if (controller_cars != null)
        {
            if (controller_cars.reachedDestination)
            {
                bool shouldBranch = false;
                // check if there's a branch on our current waypoint. if it is let's do a random chec
                // against the branch ratio to determine whether the branch should be taken or not 
                if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;

                }
                // so then if we are choosing to branch let's randomly pick a barnch as the next waypoint position
                if (shouldBranch)
                {
                    //currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
                    currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count + 1)];
                }
              
                else
                {

                    //currentWaypoint = currentWaypoint.previousWaypoint;
                    currentWaypoint = currentWaypoint.nextWaypoint;

                    /*
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        
                    }
                    */

                }

                controller_cars.SetDestination(currentWaypoint.GetPosition());
            }
        }
        else 
        {
            if (controller_pedestrians.reachedDestination)
            {
                // branch
                bool shouldBranch = false;
                if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;

                }
                if (shouldBranch)
                {
                    currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
                }

                // branch1
                bool shouldBranch1 = false;

                if (currentWaypoint.branches1 != null && currentWaypoint.branches1.Count > 0) 
                {
                    shouldBranch1 = Random.Range(0f, 1f) <= currentWaypoint.branchRatio ? true : false;
                }
                if (shouldBranch1) 
                {
                    currentWaypoint = currentWaypoint.branches1[Random.Range(0, currentWaypoint.branches1.Count - 1)];
                }


                else
                {
                    if (direction == 0) //0이면 waypoint가 감소하는 방향으로 나아간다. (nextwaypoint로)
                    {
                        if (currentWaypoint.nextWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.previousWaypoint;
                            direction = 1;
                        }
                    }
                    else if (direction == 1) //1이면 waypoint가 증가하는 방향으로 나아간다. (previouswaypoint로)
                    {
                        if (currentWaypoint.previousWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.previousWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                            direction = 0;
                        }
                    }

                }

                controller_pedestrians.SetDestination(currentWaypoint.GetPosition());
            }
        }
        
    }
}
