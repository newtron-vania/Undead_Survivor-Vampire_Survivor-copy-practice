using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public GameObject _player;
    public static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game { get { return s_instance._game; } }
    #endregion

    #region core
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
    private EventManager _event = new EventManager();
    private SceneManagerEx _scene = new SceneManagerEx();

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static EventManager Event { get { return Instance._event; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    #endregion

    public static float GameTime { get; set; } = 0;
    public static bool gameStop = false;

    void Awake()
    {
        Init();
        _player = GameObject.FindGameObjectWithTag("Player");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(_player);
    }

    static void Init()
    {
        if (s_instance == null)
        {

            GameObject go = GameObject.Find("@GameManager");
            if (go == null) 
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
            s_instance._pool.Init();
            s_instance._data.Init();
        }
    }

    private void Update()
    {
        GameTime += Time.deltaTime;
        Game.SetMousePos();
        Game.setWorldMousePos();
    }

    public static void Clear()
    {
        Sound.Clear();
        UI.Clear();
        Pool.Clear();
        Scene.Clear();
    }
    
    public static void GamePause()
    {
        Time.timeScale = 0;
        Managers.gameStop = true;
    }

    public static void GamePlay()
    {
        Time.timeScale = 1;
        Managers.gameStop = false;
    }


}
