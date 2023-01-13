using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public abstract Define.SceneType _sceneType { get; }

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));

        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

    }

    public abstract void Clear();
}
