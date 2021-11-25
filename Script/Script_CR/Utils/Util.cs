using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>(); //ItemIcon의 UI_EventHandler을 가져옴
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool reculsive = false)
    {
        //모든 gameobject는 transform을 컴포넌트로 갖기 때문에 아래의 제너릭으로 만든 함수 활용
        Transform transform = FindChild<Transform>(go, name, reculsive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool reculsive = false) where T : UnityEngine.Object
    {
        //최상위 오브젝트인 go. name의 이름을 갖는 객체를 찾는데 null로 하면 이름은 안보고 타입만 같으면 찾은 걸로 한다. 
        //reculsiver가 true이면 자식의 자식들에서도 찾지만 false이면 자식에서만 찾음
        if (go == null)
            return null;
        if (reculsive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)//직속자식 개수만큼 for
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>()) //해당 type의 컴포넌트를 모두 불러옴
            {
                if (string.IsNullOrEmpty(name) || component.name == name) //불러온 component의 이름이 찾고싶어하는 이름과 같으면 바로 반환
                    return component;
            }
        }

        return null;
    }

    // GameObject go의 child 중 T만 반환하는 list 함수
    public static List<T> FindChildTypeof<T>(GameObject go) where T : UnityEngine.Object
    {
        List<T> ChildList = new List<T>();
        Debug.Log($"Child Count {go.transform.childCount}");
        for(int i = 0; i < go.transform.childCount; i++){

            Transform transform = go.transform.GetChild(i);
            T component = transform.GetComponent<T>();
            if (component != null)
                ChildList.Add(component);
        }

        // go의 child 중 T component가 없을 경우 
        if (ChildList.Count == 0)
            return null;

        return ChildList;
    }
}
