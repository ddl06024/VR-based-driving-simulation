using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// waypointer 컨트롤하는 interface 
public class PedestriansWaypointManagerWindow_hj : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open() 
    {
        //Show existing window instance.
        GetWindow<PedestriansWaypointManagerWindow_hj>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        // SerializedObject가 GameObject랑 뭐가 다르지?
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        // waypoint가 비어있으면, helpbox 띄움.
        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
        }
        // interface 만들기
        else 
        {
            EditorGUILayout.BeginVertical("Box");
            DrawButtons();
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical("Box");
            DrawButtons1();
            EditorGUILayout.EndVertical();

        }

        obj.ApplyModifiedProperties();
    }

    void DrawButtons() 
    {
     
            
        if (GUILayout.Button("Animal Create  Waypoint"))
        {
            Create_pedestrains_Waypoint();

        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypointer_hj>())
        {
            if (GUILayout.Button("Add Branch Waypoint"))
            {
                Create_pedestrains_Branch();
            }           
            if (GUILayout.Button("Create Animals Waypoint Before"))
            {
                Create_pedestrains_waypoint_Before();
            }
            if (GUILayout.Button("Create Animals Waypoint After"))
            {
                Create_pedestrains_waypoint_After();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }


    }

    void Create_pedestrains_Waypoint() 
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Animals";

        Waypointer_hj waypoint = waypointObject.GetComponent<Waypointer_hj>();
        if (waypointRoot.childCount > 1) 
        {
            // 0부터 시작이라 -2를 해준다.
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypointer_hj>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;
            //Place the waypoint at the last position
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;

        
        }
        //Selection Class는 Editor에서(Project모든 파일) 선택된 항목에 접근을 해주는 클래스
        //지금 선택된 waypoint
        Selection.activeGameObject = waypoint.gameObject;  
    }
    void Create_pedestrains_waypoint_Before() 
    {
        GameObject waypointObject = new GameObject("waypoint" + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Animals";

        Waypointer_hj newWaypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj selectedWaypoint = Selection.activeGameObject.GetComponent<Waypointer_hj>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.position = selectedWaypoint.transform.forward;
        if (selectedWaypoint.previousWaypoint != null) 
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }
        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void Create_pedestrains_waypoint_After() 
    {
        GameObject waypointObject = new GameObject("waypoint" + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Animals";

        Waypointer_hj newWaypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj selectedWaypoint = Selection.activeGameObject.GetComponent<Waypointer_hj>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.position = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypoint = selectedWaypoint;
        if (selectedWaypoint.nextWaypoint != null) 
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }
        selectedWaypoint.nextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void RemoveWaypoint() 
    {
        Waypointer_hj selectedWaypoint = Selection.activeGameObject.GetComponent<Waypointer_hj>();

        // 맨마지막 waypoint가 아니면
        if (selectedWaypoint.nextWaypoint != null) 
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
        }
        // 맨앞의 waypoint가 아니면
        if (selectedWaypoint.previousWaypoint != null) 
        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
        }
        DestroyImmediate(selectedWaypoint.gameObject);

    }

    void Create_pedestrains_Branch() 
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Animals";

        Waypointer_hj waypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj branchedFrom = Selection.activeGameObject.GetComponent<Waypointer_hj>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }

    void Create_pedestrains_Branch1() 
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Animals";

        Waypointer_hj waypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj branchedFrom = Selection.activeGameObject.GetComponent<Waypointer_hj>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;


    }


    void DrawButtons1()
    {


        if (GUILayout.Button("Car Create  Waypoint"))
        {
            Create_cars_Waypoint();

        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypointer_hj>())
        {
            if (GUILayout.Button("Add Car Branch Waypoint"))
            {
                Create_cars_Branch();
            }
            if (GUILayout.Button("Create Car Waypoint Before"))
            {
                Create_cars_waypoint_Before();
            }
            if (GUILayout.Button("Create Car Waypoint After"))
            {
                Create_cars_waypoint_After();
            }
            if (GUILayout.Button("Remove Waypoint"))
            {
                RemoveWaypoint();
            }
        }


    }

    void Create_cars_Waypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Car";

        Waypointer_hj waypoint = waypointObject.GetComponent<Waypointer_hj>();
        if (waypointRoot.childCount > 1)
        {
            // 0부터 시작이라 -2를 해준다.
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypointer_hj>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;
            //Place the waypoint at the last position
            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;


        }
        //Selection Class는 Editor에서(Project모든 파일) 선택된 항목에 접근을 해주는 클래스
        //지금 선택된 waypoint
        Selection.activeGameObject = waypoint.gameObject;
    }
    void Create_cars_waypoint_Before()
    {
        GameObject waypointObject = new GameObject("waypoint" + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Car";

        Waypointer_hj newWaypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj selectedWaypoint = Selection.activeGameObject.GetComponent<Waypointer_hj>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.position = selectedWaypoint.transform.forward;
        if (selectedWaypoint.previousWaypoint != null)
        {
            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }
        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

    void Create_cars_waypoint_After()
    {
        GameObject waypointObject = new GameObject("waypoint" + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Car";

        Waypointer_hj newWaypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj selectedWaypoint = Selection.activeGameObject.GetComponent<Waypointer_hj>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.position = selectedWaypoint.transform.forward;

        newWaypoint.previousWaypoint = selectedWaypoint;
        if (selectedWaypoint.nextWaypoint != null)
        {
            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }
        selectedWaypoint.nextWaypoint = newWaypoint;
        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeGameObject = newWaypoint.gameObject;
    }

   

    void Create_cars_Branch()
    {
        GameObject waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypointer_hj));
        waypointObject.transform.SetParent(waypointRoot, false);
        waypointObject.gameObject.tag = "Car";

        Waypointer_hj waypoint = waypointObject.GetComponent<Waypointer_hj>();
        Waypointer_hj branchedFrom = Selection.activeGameObject.GetComponent<Waypointer_hj>();
        branchedFrom.branches.Add(waypoint);

        waypoint.transform.position = branchedFrom.transform.position;
        waypoint.transform.forward = branchedFrom.transform.forward;

        Selection.activeGameObject = waypoint.gameObject;
    }


}
