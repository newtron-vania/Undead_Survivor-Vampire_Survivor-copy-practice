using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class KnifeSpawner : WeaponSpawner
{

    float _termKnifeThrow = 0.1f;
    bool _isThrowing = false;

    void Awake()
    {
        _weaponID = 1;
    }

    void Update()
    {
        Spawn();
    }

    protected override void Spawn()
    {
        if (!_isThrowing)
        {
            StartCoroutine(KnifeThrowingOneTime());
        }

    }

    protected void SetWeaponStat(GameObject weapon)
    {
        base.SetWeaponStat();
        Knife knife = weapon.GetComponent<Knife>();
        //Create Knife to ranmdom range position
        knife.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        Vector2 dirOfPlayer = _player.GetComponent<PlayerController>()._lastDirVec;

        knife.dir = new Vector3(dirOfPlayer.x, dirOfPlayer.y, 0);

        PlayerStat playerStat = _player.GetComponent<PlayerStat>();
        knife.damage = _damage * playerStat.Attack;
        knife.speed = _movSpeed;
    }

    IEnumerator StartKnifeCoolTime()
    {

        yield return new WaitForSeconds(_cooldown);
        _isThrowing = false;
    }

     
    IEnumerator KnifeThrowingOneTime()
    {
        _isThrowing = true;
        for (int i = 0; i < _countPerCreate; i++)
        {
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Knife");
            SetWeaponStat(go);
            if (i == _countPerCreate-1)
                break;
            yield return new WaitForSeconds(_termKnifeThrow);
        }

        StartCoroutine(StartKnifeCoolTime());
    }
}