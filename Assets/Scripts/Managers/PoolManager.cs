using System.Collections.Generic;
using UnityEngine;
public class PoolManager
{
    #region Pool
    class Pool
    {
        //원본 오브젝트
        public GameObject Original { get; private set; }
        //오브젝트 풀 역할의 Root Object
        public Transform Root { get; set; }
        //Poolable Object를 저장하는 poolStack. stack이 아니라 queue를 사용해도 된다.
        Stack<Poolable> _poolStack = new Stack<Poolable>();
        //original Object를 Pooling할 Pool이 존재하지 않을 경우 Pool을 생성한다.
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{Original.name}_Root";
            for (int i = 0; i < count; i++)
                Push(Create());
        }
        //Pool에 저장되는 Poolable Object를 생성한다.
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }
        //생성된 Poolable Object를 pool에 배치한다. 
        public void Push(Poolable poolable)
        {
            //object가 존재하지 않을 시 배치하지 않는다.
            if (poolable 
                null)
                return;
            //pool역할을 하는 Object의 자식오브젝트로 배치하고, 비활성화하여 사용되지 않음을 확인시킨다.
            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.isUsing = false;
            //poolStack에 저장
            _poolStack.Push(poolable);
        }
        //pool에 배치된 오브젝트를 꺼내온다.
        public Poolable Pop(Transform parent)
        {
            Poolable poolable = null;
            //pool에 남아있는 Poolable Object가 있는지 확인한다. 없을 경우 새로 생성.
            while (_poolStack.Count > 0)
            {
                poolable = _poolStack.Pop();
                if (!poolable.gameObject.activeSelf)
                    break;
            }
            //pool에 남아있는 Object가 없다면 원본을 통해 새로 생성하여 꺼내온다.
            if (poolable == null || poolable.gameObject.activeSelf)
                poolable = Create();
            poolable.gameObject.SetActive(true);
            //부모 오브젝트로 배치되지 않을 경우 Scene 위치에 배치된다.
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.isUsing = true;
            return poolable;
        }
    }
    #endregion
    //전체 Pool을 담당하는 딕셔너리. 각 Pool마다 배치되는 Poolable Object가 다르기 때문에 각각의 원본마다 Pool을 따로 생성하고 관리해줘야한다.
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;
    public void Init()
    {
        //전체 Pool Object를 부모 오브젝트 “@Pool_Root”에 배치하여 관리한다.
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }
    //Pool사용을 다 한 Object를 해당 원본이 존재하는 Pool에 다시 배치한다.
    public void Push(Poolable poolable, float time)
    {
        //해당 원본을 담당하는 Pool이 존재하는지 확인. 존재하지 않을 경우 Poolable 	Object가 아니라는 의미이므로 해당 오브젝트를 파괴한다.
        string name = poolable.gameObject.name;
        if (!_pool.ContainsKey(name))
        {
            GameObject.Destroy(poolable.gameObject, time);
            return;
        }
        //Pool이 존재할 경우 해당 pool에 저장한다.
        _pool[name].Push(poolable);
    }
    //해당 원본에 해당하는 오브젝트를 pool에서 꺼낸다.
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        //원본에 해당되는 pool이 존재하지 않을 경우 pool을 생성
        if (!_pool.ContainsKey(original.name))
            CreatePool(original);
        //해당 pool의 Object를 꺼낸다.
        return _pool[original.name].Pop(parent);
    }
    //원본을 배치하는 pool을 생성한다.
    public void CreatePool(GameObject original, int count = 5)
    {
        //pool 객체를 생성하고 해당 원본과 지정 개수에 맞춰 pool을 초기화한다.
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;
        //해당 풀을 pool을 _pool 딕셔너리에 추가
        _pool.Add(original.name, pool);
    }
    //해당 풀의 원본을 가져온다.
    public GameObject GetOriginal(string name)
    {
        if (!_pool.ContainsKey(name))
            return null;
        return _pool[name].Original;
    }
    //모든 pool을 제거하여 초기화한다.
    public void Clear()
    {
        foreach (Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }
        _pool.Clear();
    }
}
