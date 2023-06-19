using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    #region Contents
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game { get { return _instance._game; } }
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
    public static bool _gameStop = false;

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) 
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
            _instance._sound.Init();
            _instance._pool.Init();
            _instance._data.Init();
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
        Game.Clear();
    }
    
    public static void ResetGameTime()
    {
        GameTime = 0;
    }
    public static void GamePause()
    {
        Time.timeScale = 0;
        Managers._gameStop = true;
    }

    public static void GamePlay()
    {
        Time.timeScale = 1;
        Managers._gameStop = false;
    }


}
