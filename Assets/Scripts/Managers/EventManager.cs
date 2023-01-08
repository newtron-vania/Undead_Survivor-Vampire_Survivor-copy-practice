using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    bool eventOver = false;
    public void LevelUpEvent()
    {
        Managers.UI.ShowPopupUI<UI_LevelUp>();
        Managers.GamePause();
    }

    public void LevelUpOverEvent()
    {
        Managers.Game.getPlayer().GetComponent<PlayerStat>();
        Managers.UI.ClosePopupUI(Define.PopupUIGroup.UI_LevelUp);
        Managers.GamePlay();

    }




}
