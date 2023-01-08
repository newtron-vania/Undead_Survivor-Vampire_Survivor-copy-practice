using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    public abstract void Init();

    private void Start()
    {
        Init();
    }

    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        Debug.Log($"for binding. type: {type}, uiType: {typeof(T)}. bind UI.");
        string[] name = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[name.Length];

        // when objects already was bound, don't anything
        if (_objects.ContainsKey(type))
        {
            return;
        }

        _objects.Add(type, objects);

        for (int i = 0; i < name.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, name[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, name[i], true);

            if (objects[i] == null)
                Debug.Log($"Fail to Bind {name[i]}!");
        }

        foreach (var obj in objects)
        {
            Debug.Log($"for binding. type: {type}, uiType: {typeof(T)}. UI bounded. obj: {obj}");
        }
    }

    protected T Get<T>(int idx, Type type) where T : UnityEngine.Object
    {
        Debug.Log($"for getting UI. type: {type}, uiType: {typeof(T)} index: {idx}. get UI.");

        UnityEngine.Object[] objects = _objects.GetValueOrDefault(type);

        var obj = objects[idx] as T;
        Debug.Log($"for getting UI. type: {type}, uiType: {typeof(T)} index: {idx}. got UI. obj: {obj}");

        return obj;
    }

    protected GameObject GetGameObject(int idx, Type type)
    {
        return Get<GameObject>(idx, type);
    }

    protected Text GetText(int idx, Type type)
    {
        return Get<Text>(idx, type);
    }

    protected Button GetButton(int idx, Type type)
    {
        return Get<Button>(idx, type);
    }

    protected Image GetImage(int idx, Type type)
    {
        return Get<Image>(idx, type);
    }

    public static void BindUIEvent(GameObject go, Action<PointerEventData> action,
        Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}