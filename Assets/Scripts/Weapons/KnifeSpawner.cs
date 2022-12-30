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
    }

    protected void SetWeaponStat(Knife knife)
    {
        knife.transform.position = transform.position;
        PlayerController controller = _player.GetComponent<PlayerController>();
        Vector2 dirOfPlayer = controller._lastDirVec;

        knife.dir = new Vector3(dirOfPlayer.x, dirOfPlayer.y, 0);

        PlayerStat playerStat = _player.GetComponent<PlayerStat>();
        knife.damage = knifeStat[level].damage * playerStat.Attack;
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.05f);
    }
}