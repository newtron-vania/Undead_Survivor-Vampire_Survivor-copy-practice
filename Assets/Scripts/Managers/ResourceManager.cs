using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    

    //Original을 찾아서 반환
    public T Load<T>(string path) where T:Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Faild to load prefab : {path}");
            return null;
        }

        //풀링 가능한 객체인지 확인
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;

    }

    public void Destroy(GameObject obj, float time = 0)
    {
        if(obj == null)
        {
            return;
        }

        //만약 풀링된 오브젝트라면
        Poolable poolable = obj.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(obj, time);
    }
}
