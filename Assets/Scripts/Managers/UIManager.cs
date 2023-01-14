using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    private int _order = 10;

    private Dictionary<Define.PopupUIGroup, Stack<UI_Popup>> _popupStackDict = new Dictionary<Define.PopupUIGroup, Stack<UI_Popup>>();
    private UI_Scene _sceneUI = null;

    public GameObject Root()
    {
        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
            root = new GameObject { name = "@UI_Root" };

        return root;
    }

    public void SetCanvas(GameObject go, bool sorting = false)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sorting)
            canvas.sortingOrder = _order++;
        else
            canvas.sortingOrder = 0;
    }
    
    public T MakeWorldSpaceUI<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/WorldSpace/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        //return go.GetOrAddComponent<T>();
        return Util.GetOrAddComponent<T>(go);
    }
    
    public T MakeSubItem<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if(parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }
    
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;


        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;


        go.transform.SetParent(Root().transform);
        return sceneUI;
    }
    public UI_Scene getSceneUI()
    {
        Debug.Log($"sceneUI - {_sceneUI.name}");
        return _sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        
        go.transform.SetParent(Root().transform);
        
        T popup =  go.GetOrAddComponent<T>();
        Define.PopupUIGroup popupType = popup._popupID;

        if (!_popupStackDict.ContainsKey(popupType))
            _popupStackDict.Add(popupType, new Stack<UI_Popup>());
            
        _popupStackDict[popupType].Push(popup);

        
        return popup as T;
    }

    public void ClosePopupUI(Define.PopupUIGroup popupType)
    {
        if (_popupStackDict.TryGetValue(popupType, out Stack<UI_Popup> popupStack) == false
            || _popupStackDict[popupType].Count == 0)
            return;

        UI_Popup popup = _popupStackDict[popupType].Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        CheckPopupUICountAndRemove();
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        Define.PopupUIGroup popupType = popup._popupID;
        if (_popupStackDict.TryGetValue(popupType, out Stack<UI_Popup> popupStack) == false
            || _popupStackDict[popupType].Count == 0)
            return;

        if (popup != popupStack.Peek())
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI(popupType);
    }
    public void CloseAllPopupUI()
    {
        foreach (KeyValuePair<Define.PopupUIGroup, Stack<UI_Popup>> kv in _popupStackDict)
        {
            Define.PopupUIGroup popupType = kv.Key;
            Stack<UI_Popup> popupStack = kv.Value;
            while(popupStack.Count != 0)
            {
                UI_Popup popup = popupStack.Pop();
                Managers.Resource.Destroy(popup.gameObject);
                popup = null;
            }
        }
        CheckPopupUICountAndRemove();
    }

    public void CloseAllGroupPopupUI(Define.PopupUIGroup popupType)
    {
        if (_popupStackDict.TryGetValue(popupType, out Stack<UI_Popup> popupStack) == false
            || _popupStackDict[popupType].Count == 0)
            return;

        while (popupStack.Count != 0)
        {
            UI_Popup popup = popupStack.Pop();
            Managers.Resource.Destroy(popup.gameObject);
            popup = null;
        }
        CheckPopupUICountAndRemove();
    }


    void CheckPopupUICountAndRemove()
    {
        List<Define.PopupUIGroup> popupType = new List<Define.PopupUIGroup>();
        foreach(Define.PopupUIGroup popupUI in _popupStackDict.Keys)
        {
            popupType.Add(popupUI);
        }
        for(int i = 0; i<_popupStackDict.Count; i++)
        {
            if (_popupStackDict.GetValueOrDefault<Define.PopupUIGroup, Stack<UI_Popup>>(popupType[i]).Count == 0)
                _popupStackDict.Remove(popupType[i]);
        }
        CheckPopupUICountInScene();
    }
    
    void CheckPopupUICountInScene()
    {

        Debug.Log($"popupCount : {_popupStackDict.Count}");
        foreach(Define.PopupUIGroup popupKey in _popupStackDict.Keys)
        {
            Debug.Log($"popupList : {popupKey}");
        }
            
        if (_popupStackDict.Count == 0)
        {
            Managers.GamePlay();
        }
    }

    public int GetPopupUICount()
    {
        return _popupStackDict.Count;
    }
    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
