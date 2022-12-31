using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    public int damage = 10;
    [SerializeField] private float createTime = 0f;

    private void Start()
    {
        Debug.Log("Spin weapon started");
    }

    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            EnemyStat stat = go.GetComponent<EnemyStat>();
            stat.HP -= Mathf.Max(damage - stat.Defense, 1);
            Debug.Log($"{this.name} damaged to the enemy. enemy's hp is ${stat.HP}");

            stat.OnDead();
        }
    }
}