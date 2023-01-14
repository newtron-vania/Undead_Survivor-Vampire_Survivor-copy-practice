using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : WeaponController
{

    bool _isCool = false;
    Transform _damageEffectImage;

    public override int _weaponType { get { return (int)Define.Weapons.Poison; } }
    private void Start()
    {
        _damageEffectImage = transform.GetChild(0);
        Debug.Log(_damageEffectImage);
    }

    private void Update()
    {
        if (!_isCool)
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
        _damageEffectImage.localScale = Vector3.one * _size * 2;
    }

    IEnumerator DamageCoolTime()
    {
        _isCool = true;
        yield return new WaitForSeconds(_cooldown);
        _isCool = false;
    }
}
