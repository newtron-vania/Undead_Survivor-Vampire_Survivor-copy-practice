using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    Dictionary<int, Data.WeaponLevelData> _posionWeapon
    public int damage = 10;
    private float createTime = 0f;


    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            EnemyStat stat = go.GetComponent<EnemyStat>();
            stat.HP -= Mathf.Max(damage - stat.Defense, 1);

            go.GetComponent<EnemyController>().OnDead();
        }
    }
}