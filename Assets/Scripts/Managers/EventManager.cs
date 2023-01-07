using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    enum PlayerStats
    {
        MaxHP,
        MoveSpeed,
        Damage,
        Defense,
    }

    enum PlayerWeapons
    {
        Knife,
        fireball,
        spin,
        poison,
        lightning,
        shotgun
    }
    public List<int> SetRandomItem(int Maxcount)
    {
        List<int> randomItem = new List<int>();
        int i = 0;
        while(i < Maxcount)
        {
            int rd = Random.Range(1, 2);
            randomItem.Add(rd);
            i++;
        }
        return randomItem;
    }

    public void SetRandomStat(PlayerStat player)
    {
        int _statNum = Random.Range(0, 3);
        PlayerStats playerStats = (PlayerStats)_statNum;
        switch (playerStats)
        {
            case PlayerStats.Damage:
                player.Damage += 1;
                break;
            case PlayerStats.Defense:
                player.Defense += 1;
                break;
            case PlayerStats.MaxHP:
                player.MaxHP += 10;
                break;
            case PlayerStats.MoveSpeed:
                player.MoveSpeed += 1;
                break;
        }
    }

    public void SetRandomWeapon(PlayerStat player)
    {
        int _weaponNum = Random.Range(0, 5);
        PlayerWeapons playerWeapon = (PlayerWeapons)_weaponNum;
        int value = 0;
        if (player.WeaponDict.TryGetValue((int)playerWeapon, out value))
        {
            player.WeaponDict[(int)playerWeapon] += 1;
            return;
        }
        player.WeaponDict.Add((int)playerWeapon, 1);

    }

    void LevelUpEvent(bool isLevelUp)
    {
        if (!isLevelUp)
        {
            //EventController's Event(if player level up, create Random Stat Selector UI)
            //when select is done, despawn UI, and check level up is over.
            List<int> randItemValue = Managers.Event.SetRandomItem(3);
            foreach (int itemValue in randItemValue)
            {
                switch (itemValue)
                {
                    case 1:
                        //Managers.Event.SetRandomStat();
                        break;
                    case 2:
                        //Managers.Event.SetRandomWeapon();
                        break;
                }
            }
            //LevelUp
            //_level += 1;
            //_exp -= _maxExp;
            //_maxExp *= 2;
        }
    }




}
