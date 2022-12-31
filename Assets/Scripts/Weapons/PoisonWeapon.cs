using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWeapon : MonoBehaviour
{
    int _damage = 5;
    float _coolTime = 1f;
    bool _isAttack = false;


    private void Update()
    {
        if (!_isAttack)
        {
            StartCoroutine(DamageCoolTime());
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f, LayerMask.GetMask("Enemy"));
            foreach(Collider2D coll in collider2Ds)
            {
                GameObject go = coll.gameObject;
                go.GetComponent<EnemyController>().OnDamaged(_damage);
            }
            Debug.Log($"Poison Field is Activated! Monster {collider2Ds.Length} count are received {_damage} damage!");
        }

    }

    IEnumerator DamageCoolTime()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_coolTime);
        _isAttack = false;
    }
}
