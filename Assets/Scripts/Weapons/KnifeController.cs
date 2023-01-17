using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class KnifeController : WeaponController
{

    float _termKnifeThrow = 0.1f;
    bool _isCool = false;

    public override int _weaponType { get { return (int)Define.Weapons.Knife; } }

    void Update()
    {
        if (!_isCool)
        {
            StartCoroutine(SpawnWeapon());
        }
    }


    IEnumerator StartKnifeCoolTime()
    {

        yield return new WaitForSeconds(_cooldown);
        _isCool = false;
    }

     
    IEnumerator SpawnWeapon()
    {
        _isCool = true;
        for (int i = 0; i < _countPerCreate; i++)
        {
            Managers.Sound.Play("Shoot_01");
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Knife");
            SetWeapon(go);
            if (i == _countPerCreate-1)
                break;
            yield return new WaitForSeconds(_termKnifeThrow);
        }

        StartCoroutine(StartKnifeCoolTime());
    }

    protected void SetWeapon(GameObject weapon)
    {
        Knife knife = weapon.GetComponent<Knife>();
        //Create Knife to ranmdom range position
        knife.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        Vector2 dirOfPlayer = _player.GetComponent<PlayerController>()._lastDirVec;

        knife.dir = new Vector3(dirOfPlayer.x, dirOfPlayer.y, 0);

        knife.damage = _damage;
        knife.speed = _movSpeed;
        knife.force = _force;
    }
}