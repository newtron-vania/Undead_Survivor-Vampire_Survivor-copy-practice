using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : WeaponSpawner
{
    List<Data.WeaponLevelData> knifeStat;



    float time = 0;
    void Start()
    {
        knifeStat = weaponData[1].weaponLevelData;
    }

    void Update()
    {

        if (Managers.GameTime - time > knifeStat[level].cooldown)
        {
            for (int i = 0; i < knifeStat[level].createPerCount; i++)
            {
                Spawn();
                StartCoroutine(SpawnDelay());
            }

            time = Managers.GameTime;
        }
    }

    protected override void Spawn()
    {
        GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Knife");
        Knife knifeStat = go.GetComponent<Knife>();
        SetWeaponStat(knifeStat);
        go.transform.position = transform.position;
        if (_player.GetComponent<SpriteRenderer>().flipX)
            go.GetComponent<Knife>().dir = new Vector3(-1, 0, 0);
        else
            go.GetComponent<Knife>().dir = new Vector3(1, 0, 0);
    }

    protected void SetWeaponStat(Knife knife)
    {
        PlayerStat playerStat = _player.GetComponent<PlayerStat>();
        knife.damage = knifeStat[level].damage * playerStat.Attack;
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.05f);
    }
}
