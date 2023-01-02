using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class KnifeSpawner : WeaponSpawner
{
    Dictionary<int, Data.WeaponLevelData> knifeStat;

    float _cooldown = 2;
    float _termKnifeThrow = 0.1f;
    bool _isThrowing = false;


    void Start()
    {
        _weaponID = 1;
        knifeStat = MakeLevelDataDict(1);
        _cooldown = knifeStat[_level].cooldown;
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

    protected override void SetWeaponStat(GameObject weapon)
    {
        base.SetWeaponStat(weapon);
        Knife knife = weapon.GetComponent<Knife>();
        //Create Knife to ranmdom range position
        knife.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        Vector2 dirOfPlayer = _player.GetComponent<PlayerController>()._lastDirVec;

        knife.dir = new Vector3(dirOfPlayer.x, dirOfPlayer.y, 0);

        PlayerStat playerStat = _player.GetComponent<PlayerStat>();
        knife.damage = knifeStat[_level].damage * playerStat.Attack;
        knife.speed = knifeStat[_level].movSpeed;
        _cooldown = knifeStat[_level].cooldown;
    }

    IEnumerator StartKnifeCoolTime()
    {

        yield return new WaitForSeconds(_cooldown);
        _isThrowing = false;
    }

    IEnumerator KnifeThrowingOneTime()
    {
        _isThrowing = true;
        for (int i = 0; i < knifeStat[_level].createPerCount; i++)
        {
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Knife");
            SetWeaponStat(go);
            if (i == knifeStat[_level].createPerCount)
                break;
            yield return new WaitForSeconds(_termKnifeThrow);
        }

        StartCoroutine(StartKnifeCoolTime());
    }
}