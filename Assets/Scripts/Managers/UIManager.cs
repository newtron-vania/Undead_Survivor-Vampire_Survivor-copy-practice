using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Canvas _canvas = null;
    //0 : popup else : full
    List<string> _currentUI = new List<string>();
    private Stack<GameObject> _popUp = new Stack<GameObject>();
    private List<GameObject> _fullUI = new List<GameObject>();

    public void Init()
    {
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (_canvas == null)
        {
            _canvas = null;
        }
    }

    public T SetWorldSpaceUI<T>(Transform parent, string name = null) where T : UnityEngine.Component
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");
        
        if(parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Util.GetOrAddComponent<T>(go);

    }
    
    
    public void ShowPopupUI(string name)
    {
        if (_currentUI.Count == 0)
            Managers.GamePause();
        GameObject go =  _canvas.transform.Find(name).gameObject;
        _currentUI.Add("0");
        _popUp.Push(go);
        go.SetActive(true);
    }
    
    public void ShowFullUI(string name)
    {
        GameObject go =  _canvas.transform.Find(name).gameObject;
        _currentUI.Add(go.name);
        _fullUI.Add(go);
        go.SetActive(true);
    }

    public void CloseCurUI()
    {
        if (_currentUI.Count == 0)
            return;
        string value = _currentUI[_currentUI.Count];
        _currentUI.RemoveAt(_currentUI.Count-1);
        if (value == "0")
        {
            GameObject go = _popUp.Pop();
            go.SetActive(false);
        }
        else
        {
            GameObject go = _fullUI.Find(x => x.name == value);
            _fullUI.Remove(go);
            go.SetActive(false);
        }

        if (_currentUI.Count == 0)
            Managers.GamePlay();
    }
    
    public void CloseUI(string name)
    {
        GameObject go =  _fullUI.Find(x => x.name == name);
        _currentUI.Remove(name);
        go.SetActive(false);
    }

    public void CloseAllUI()
    {
        while (_currentUI.Count > 0)
        {
            CloseCurUI();
        }
    }

    public void Clear()
    {
        CloseAllUI();
    }
}
