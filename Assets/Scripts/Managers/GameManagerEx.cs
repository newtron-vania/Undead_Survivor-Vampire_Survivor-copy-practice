using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManagerEx
{
    GameObject _player;

    // int <-> gameobject
    //Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monster = new HashSet<GameObject>();
    public Action<int> OnSpawnEvent;

    public Vector3 MousePos { get; set; }
    public Vector3 WorldMousePos { get; set; }

    public GameObject getPlayer() { return _player; }
    public Data.Player StartPlayer { get; set; } = new Data.Player();
    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Unknown:
                break;
            case Define.WorldObject.Enemy:
                _monster.Add(go);
                OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;

    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        Define.WorldObject type = go.GetComponent<BaseController>()._type;

        if(go == null)
            return Define.WorldObject.Unknown;

        return type;

    }

    public void Despawn(GameObject go, float time = 0)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Player:
                if (_player == go)
                {
                    _player = null;
                }
                break;
            case Define.WorldObject.Enemy:
                {
                    if (_monster.Contains(go))
                    {
                        _monster.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
        }
        Managers.Resource.Destroy(go, time);
    }

    public void Clear()
    {
        Despawn(_player);
    }

    public void SetMousePos()
    {
        MousePos = Input.mousePosition;
    }

    public void setWorldMousePos()
    {
        WorldMousePos = Camera.main.ScreenToWorldPoint(MousePos) + new Vector3(0,0,10);
    }

}
