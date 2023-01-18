using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{

    public int damage = 10;
    public float force = 0f;
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
            go.GetComponent<BaseController>().OnDamaged(damage, force);
        }
    }
}