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


    public List<string[]> SetRandomItem(PlayerStat player, int Maxcount)
    {
        int i = 0;
        List<string[]> PoolList = new List<string[]>();
        while(i < Maxcount)
        {
            string[] selected = new string[2];
            int rd = Random.Range(1, 3);
            if (rd == 1)
            {
                selected[0] = "1";
                selected[1] = SetRandomStat(player);
            }

            else
            {
                selected[0] = "2";
                selected[1] = SetRandomWeapon(player);
            }

            if (PoolList.Contains(selected))
                continue;
            PoolList.Add(selected);
            i++;
        }
        return PoolList;
    }

    public string SetRandomStat(PlayerStat player)
    {
        int _statNum = Random.Range(0, System.Enum.GetValues(typeof(PlayerStats)).Length);
        PlayerStats playerStats = (PlayerStats)_statNum;
        return playerStats.ToString();
    }

    public string SetRandomWeapon(PlayerStat player)
    {
        int weaponNum = Random.Range(1, System.Enum.GetValues(typeof(Define.Weapons)).Length+1);
        Define.Weapons playerWeapon = (Define.Weapons)weaponNum;
        return playerWeapon.ToString();

    }

    bool eventOver = false;
    public void LevelUpEvent()
    {
        Managers.UI.ShowPopupUI<UI_LevelUp>();
        Managers.GamePause();
    }

    public void LevelUpOverEvent(int itemType, string itemName)
    {
        //PlayerStatorWeaponUp
        PlayerStat player = Managers.Game.getPlayer().GetComponent<PlayerStat>();
        if (itemType == 1)
        {
            switch (itemName)
            {
                case "MaxHP":
                    player.MaxHP += 10;
                    player.HP = player.MaxHP;
                    Debug.Log($"HP up! {player.MaxHP}");
                    break;
                case "MoveSpeed":
                    player.MoveSpeed += 1;
                    Debug.Log($"MoveSpeed up! {player.MoveSpeed}");
                    break;
                case "Damage":
                    player.Damage += 1;
                    Debug.Log($"Damage up! {player.Damage}");
                    break;
                case "Defense":
                    player.Defense += 1;
                    Debug.Log($"Defense up! {player.Defense}");
                    break;
            }
            player.AddOrSetWeaponDict(Define.Weapons.shotgun, 0);
        }
            
        else
        {
            Define.Weapons weaponType = (Define.Weapons)System.Enum.Parse(typeof(Define.Weapons), itemName);
            player.AddOrSetWeaponDict(weaponType, 1);
            Debug.Log($"weapon up! {player.GetWeaponDict()[weaponType]}");
        }
            

        Managers.UI.ClosePopupUI(Define.PopupUIGroup.UI_LevelUp);
        Managers.GamePlay();

    }




}
