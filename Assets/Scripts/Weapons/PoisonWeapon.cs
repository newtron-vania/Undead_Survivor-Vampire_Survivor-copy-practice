using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWeapon : WeaponSpawner
{
    Dictionary<int, Data.WeaponLevelData> _posionWeapon;
    bool _isAttack = false;

    private void Start()
    {
        _posionWeapon = MakeLevelDataDict(4);
    }

    private void Update()
    {
        if (!_isAttack)
        {
            StartCoroutine(DamageCoolTime());
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll((Vector2)transform.position, 3f, LayerMask.GetMask("Enemy"));
            foreach(Collider2D coll in collider2Ds)
            {
                GameObject go = coll.gameObject;
                go.GetComponent<EnemyController>().OnDamaged(_posionWeapon[level].damage);
            }
        }

    }

    protected override void SetWeaponStat(GameObject weapon)
    {
        base.SetWeaponStat(weapon);
    }

    IEnumerator DamageCoolTime()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_posionWeapon[level].cooldown);
        _isAttack = false;
    }
}
