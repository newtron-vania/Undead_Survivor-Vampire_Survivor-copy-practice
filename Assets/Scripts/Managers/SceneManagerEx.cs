using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{

    public BaseScene CurrentScene {get{ return GameObject.FindObjectOfType<BaseScene>(); }}
   public void LoadScene(Define.SceneType type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.SceneType type)
    {
        string name = System.Enum.GetName(typeof(Define.SceneType), type);
        return name;
    }
    public void Clear()
    {
        CurrentScene.Clear();
    }
}
