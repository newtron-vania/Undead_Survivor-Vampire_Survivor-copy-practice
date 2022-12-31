using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class KnifeSpawner : WeaponSpawner
{
    List<WeaponLevelData> knifeStat;


    float time = 0;

    void Start()
    {
        knifeStat = weaponData[1].weaponLevelData;
    }

    void Update()
    {
        Spawn();
    }

    protected override void Spawn()
    {
        if (Managers.GameTime - time > knifeStat[level].cooldown)
        {
            for (int i = 0; i < knifeStat[level].createPerCount; i++)
            {
                GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Knife");
                SetWeaponStat(go);
            }
            time = Managers.GameTime;
        }
        
    }

    protected override void SetWeaponStat(GameObject weapon)
    {
        Knife knife = weapon.GetComponent<Knife>();
        //Create Knife to ranmdom range position
        knife.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        Vector2 dirOfPlayer = _player.GetComponent<PlayerController>()._lastDirVec;

        knife.dir = new Vector3(dirOfPlayer.x, dirOfPlayer.y, 0);

        PlayerStat playerStat = _player.GetComponent<PlayerStat>();
        knife.damage = knifeStat[level].damage * playerStat.Attack;
        knife.speed = knifeStat[level].movSpeed;
    }

}