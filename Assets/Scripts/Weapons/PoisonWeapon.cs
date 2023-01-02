using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWeapon : WeaponSpawner
{

    bool _isAttack = false;
    Transform _damageEffectImage;

    private void Awake()
    {
        _weaponID = 4;
        _damageEffectImage = transform.GetChild(0);
    }

    private void Update()
    {
        SetWeaponStat(gameObject);
        if (!_isAttack)
        {
            StartCoroutine(DamageCoolTime());
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll((Vector2)transform.position, _size, LayerMask.GetMask("Enemy"));
            foreach(Collider2D coll in collider2Ds)
            {
                GameObject go = coll.gameObject;
                go.GetComponent<EnemyController>().OnDamaged(_damage);
            }
        }

    }

    protected void SetWeaponStat(GameObject weapon)
    {
        base.SetWeaponStat();
        _damageEffectImage.localScale = Vector3.one * _size * 2;
    }

    IEnumerator DamageCoolTime()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_cooldown);
        _isAttack = false;
    }
}
