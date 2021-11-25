using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCharacterController : MonoBehaviour
{
    public WaypointNavigator waypointnavigator;
    public Animator animator;

    void Start()
    {
        // 이전의 애니메이터 가져와서 붙이니다
        waypointnavigator = gameObject.GetComponent<WaypointNavigator>();
        animator = GetComponent<Animator>();
        // 애니메이터를 뛰는 걸로 바꾼다
        // 스피드 변경한다. 

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (waypointnavigator.shouldBranch1)
        {
            // run으로 바꾸기
            animator =
            
        }
        else 
        {
            // walk로 바꾸기
            animator = 
        }
        */
    }
}
