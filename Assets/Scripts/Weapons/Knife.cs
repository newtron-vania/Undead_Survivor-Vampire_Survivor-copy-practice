using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Vector3 dir = new Vector3();
    public int damage = 10;
    public float speed = 10f;
    float lifeTime = 3f;
    float createTime = 0f;


    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }
    void Update()
    {
        if (Managers.GameTime - createTime > lifeTime)
        {
            Debug.Log("Knife lifetime is over!");
            Managers.Resource.Destroy(gameObject);
        }
            
        transform.position += dir * speed * Time.deltaTime;     
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Enemy")
        {
            EnemyStat _stat = go.GetComponent<EnemyStat>();
            _stat.HP -= Mathf.Max(damage - _stat.Defense, 1);
            Managers.Resource.Destroy(gameObject);
            Debug.Log(damage);
        }
    }
}
