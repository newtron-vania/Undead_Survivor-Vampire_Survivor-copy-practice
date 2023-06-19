using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : MonoBehaviour
{

    public int _damage = 10;
    public float _force = 0f;

    public float _createTime = 0f;


    private void OnEnable()
    {
        _createTime = Managers.GameTime;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            go.GetComponent<BaseController>().OnDamaged(_damage, _force);
        }
    }
}