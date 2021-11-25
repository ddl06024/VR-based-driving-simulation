using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    // DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    // PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();
    PlayStateManager _state = new PlayStateManager();
    ScoreManager _score = new ScoreManager(); // sound
    SoundManager _sound = new SoundManager(); // score


    // public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    // public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    // public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

    // Play State Manager
    public static PlayStateManager State { get { return Instance._state; } }
    // Score Manager
    public static ScoreManager Score { get { return Instance._score; } }
    // Sound Manager
    public static SoundManager Sound { get { return Instance._sound; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        // Input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            //s_instance._data.Init();
            //s_instance._pool.Init();
            s_instance._sound.Init();
            s_instance._state.Init();

        }
    }

    public static void Clear()
    {

        // Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();

        // Pool.Clear();
    }
}