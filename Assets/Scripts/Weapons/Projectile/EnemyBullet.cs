using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 _dir = new Vector3(1, 0, 0);

    [SerializeField] 
    private float lifeTime = 2f;
    [SerializeField]
    private float createTime = 0f;

    public int _damage = 3;
    public float _speed = 6f;

    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }

    void FixedUpdate()
    {
        if (Managers.GameTime - createTime > lifeTime)
        {
            Managers.Resource.Destroy(gameObject);
        }
        OnMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Player"))
        {
            go.GetComponent<BaseController>().OnDamaged(_damage);
            Managers.Resource.Destroy(gameObject);
        }
    }


    void OnMove()
    {
        transform.position += _dir * (_speed * Time.fixedDeltaTime);
    }
}
