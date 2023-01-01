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

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SoundManager Sound { get { return Instance._sound; } }

    #endregion

    public static float GameTime { get; set; } = 0;
    public static bool gameStop = false;
    public TextMeshProUGUI GameTimeText;

    void Awake()
    {
        Init();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    static void Init()
    {
        if (s_instance == null)
        {

            //매니저 초기화
            GameObject go = GameObject.Find("@GameManager");
            if (go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<Managers>();
            }
            //삭제되지 않게끔 설정 -> Scene 이동을 하더라도 파괴되지 않음
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
        int minute = 0;
        int second = 0;
        minute = Mathf.FloorToInt(GameTime / 60);
        second = Mathf.FloorToInt(GameTime % 60);
        GameTimeText.text = string.Format("{0:D2}:{0:D2}", minute, second);
    }

    public static void Clear()
    {
        Sound.Clear();
        UI.Clear();
        Pool.Clear();
    }

    public static void GamePause()
    {
        Time.timeScale = 0;
        gameStop = true;
    }

    public static void GamePlay()
    {
        Time.timeScale = 1;
        gameStop = false;
    }
}
