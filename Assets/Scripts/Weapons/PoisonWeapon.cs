using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWeapon : WeaponSpawner
{
    Dictionary<int, Data.WeaponLevelData> _posionWeapon;
    int _damage = 0;
    float _range = 0;
    float _cooldown = 0;

    bool _isAttack = false;
    Transform _damageEffectImage;

    private void Start()
    {
        _posionWeapon = MakeLevelDataDict(4);
        _damageEffectImage = transform.GetChild(0);
    }

    private void Update()
    {
        SetWeaponStat(gameObject);
        if (!_isAttack)
        {
            StartCoroutine(DamageCoolTime());
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll((Vector2)transform.position, _range, LayerMask.GetMask("Enemy"));
            foreach(Collider2D coll in collider2Ds)
            {
                GameObject go = coll.gameObject;
                go.GetComponent<EnemyController>().OnDamaged(_damage);
            }
        }

    }

    protected override void SetWeaponStat(GameObject weapon)
    {
        base.SetWeaponStat(weapon);
        _damage = _posionWeapon[level].damage;
        _range = _posionWeapon[level].size;
        _cooldown = _posionWeapon[level].cooldown;
        _damageEffectImage.localScale = Vector3.one * _range * 2;
    }

    IEnumerator DamageCoolTime()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_cooldown);
        _isAttack = false;
    }
}
