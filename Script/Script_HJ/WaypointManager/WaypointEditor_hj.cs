using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Unity 에디터의 기동시, 스크립트의 컴파일 직후에 클래스의 static 생성자를 불러내기 위한 속성
[InitializeOnLoad()]
//seeing how each of our waypoints are connected
public class WaypointEditor_hj
{
    // add the draw gizmo attribute and add gizmo type non-selected gizmo, and pickable
    // to draw  the gizmo regardless of whether or not th game object is selected
    // and by declaring this as pickable were also telling unity to make the gizmo selectable att he start 
    // 사람
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypointer_hj waypoint, GizmoType gizmoType)
    {

        // 색으로 gameobject가 selected되었는지 확인한다
        // select되었으면 진하게 
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        // 안되었으면, 투명하게
        else
        {
            Gizmos.color = Color.yellow * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, .1f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.width / 2f), waypoint.transform.position - (waypoint.transform.right * waypoint.width / 2f));

        Waypointer_hj waypo = waypoint.GetComponent<Waypointer_hj>();
        


        if (waypoint.previousWaypoint != null && (waypo.tag == "Animals" || waypo.tag == "Untagged"))
        {
            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);
        }

        if (waypoint.nextWaypoint != null && (waypo.tag == "Animals" || waypo.tag == "Untagged"))
        {
            Gizmos.color = Color.green;
            Vector3 offset = waypoint.transform.right * -waypoint.width / 2f;
            Vector3 offsetTo = waypoint.nextWaypoint.transform.right * -waypoint.nextWaypoint.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaypoint.transform.position + offsetTo);
        }

        if (waypoint.previousWaypoint != null && waypo.tag == "Car")
        {
            Gizmos.color = Color.yellow;
            Vector3 offset = waypoint.transform.right * waypoint.width / 2f;
            Vector3 offsetTo = waypoint.previousWaypoint.transform.right * waypoint.previousWaypoint.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);
        }

        if (waypoint.nextWaypoint != null && waypo.tag == "Car")
        {
            Gizmos.color = Color.gray;
            Vector3 offset = waypoint.transform.right * -waypoint.width / 2f;
            Vector3 offsetTo = waypoint.nextWaypoint.transform.right * -waypoint.nextWaypoint.width / 2f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.nextWaypoint.transform.position + offsetTo);
        }

        if (waypoint.branches != null )
        {
            foreach (Waypointer_hj branch in waypoint.branches)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
            }
        }

        if (waypoint.branches1 != null)
        {
            foreach (Waypointer_hj branch1 in waypoint.branches1)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawLine(waypoint.transform.position, branch1.transform.position);
            }
        }

    }

    
};
