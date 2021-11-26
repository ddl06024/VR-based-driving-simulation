using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{
    Level_1,
    Level_2,
    Level_3
}
public class Player_Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;
    private GameObject child;
    private Quaternion rotate;
    public Level level;
    private BoxCollider area;

    // GameObject 가져오기 (게임 시작 전에 제일 먼저 시작)
    private void Awake()
    {
        // Prefab에서 load
        Player = Resources.Load<GameObject>("chiron_Park");
        Debug.Log(Player);
        Debug.Log($"Level {level}");


    }
    void Start()
    {
        // spawn할 area 설정
        child = GameObject.Find(level.ToString());
        Vector3 currentPosition = child.GetComponent<Transform>().position;

        switch (level)
        {
            case Level.Level_1:
                rotate = Quaternion.Euler(0, 180, 0);
                break;

            case Level.Level_2:
                rotate = Quaternion.identity;
                break;

            case Level.Level_3:
                rotate = Quaternion.Euler(0, 90, 0);
                break;

        }
        Instantiate(Player, currentPosition, rotate);
    }

    public GameObject GetPlayer()
    {
        return Player;
    }

}
