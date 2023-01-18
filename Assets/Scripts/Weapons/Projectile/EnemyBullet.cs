using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 dir = new Vector3(1, 0, 0);
    public int damage = 3;
    public float speed = 6f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float createTime = 0f;


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
            go.GetComponent<BaseController>().OnDamaged(damage);
            Managers.Resource.Destroy(gameObject);
        }
    }


    void OnMove()
    {
        transform.position += dir * (speed * Time.fixedDeltaTime);
    }
}
