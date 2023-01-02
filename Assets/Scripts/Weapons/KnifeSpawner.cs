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
        knifeStat = MakeLevelDataDict(1);
        _cooldown = knifeStat[level].cooldown;
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
        knife.damage = knifeStat[level].damage * playerStat.Attack;
        knife.speed = knifeStat[level].movSpeed;
        _cooldown = knifeStat[level].cooldown;
    }

    IEnumerator StartKnifeCoolTime()
    {

        yield return new WaitForSeconds(_cooldown);
        _isThrowing = false;
    }

    IEnumerator KnifeThrowingOneTime()
    {
        _isThrowing = true;
        for (int i = 0; i < knifeStat[level].createPerCount; i++)
        {
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Knife");
            SetWeaponStat(go);
            Debug.Log($"knife Thrown {i + 1} per {_termKnifeThrow} second!");
            if (i == knifeStat[level].createPerCount)
                break;
            yield return new WaitForSeconds(_termKnifeThrow);
        }
        Debug.Log($"knife Throwing over! cooltime start : {_cooldown}");

        StartCoroutine(StartKnifeCoolTime());
    }
}