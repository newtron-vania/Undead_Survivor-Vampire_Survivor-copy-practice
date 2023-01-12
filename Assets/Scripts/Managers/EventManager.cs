using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EventManager
{
    public enum PlayerStats
    {
        MaxHP,
        MoveSpeed,
        Damage,
        Defense,
        Cooldown,
        Amount
    }


    public List<string[]> SetRandomItem(PlayerStat player, int Maxcount)
    {
        int i = 0;
        List<string[]> PoolList = new List<string[]>();
        while(i < Maxcount)
        {
            string[] selected = new string[2];
            float random = Random.Range(0, 100);
            int rd = 0;

            if (random < 10)
                rd = 0;
            else if (random < 40)
                rd = 1;
            else
                rd = 2;
            if (rd== 0)
            {
                if (player.GetWeaponDict()[player.playertStartWeapon] >= 5)
                    continue;
                selected[0] = "0";
                selected[1] = player.playertStartWeapon.ToString();
            }
            if (rd == 1)
            {
                selected[0] = "1";
                PlayerStats stat = SetRandomStat();
                selected[1] = stat.ToString();
            }

            else
            {
                selected[0] = "2";
                Define.Weapons weapon = SetRandomWeapon();
                if (player.GetWeaponDict().GetValueOrDefault<Define.Weapons, int>(weapon) >= 5)
                    continue;
                selected[1] = weapon.ToString();
            }
            bool isContains = false;
            foreach (string[] type in PoolList)
            {
                if(selected[1] == type[1])
                {
                    isContains = true;
                    break;
                }
            }
            if (isContains)
                continue;
            PoolList.Add(selected);
            i++;
        }
        return PoolList;
    }

    public PlayerStats SetRandomStat()
    {
        int _statNum = Random.Range(0, System.Enum.GetValues(typeof(PlayerStats)).Length);
        PlayerStats playerStats = (PlayerStats)_statNum;
        return playerStats;
    }

    public Define.Weapons SetRandomWeapon()
    {
        int weaponNum = Random.Range(1, System.Enum.GetValues(typeof(Define.Weapons)).Length+1 - System.Enum.GetValues(typeof(Define.PlayerStartWeapon)).Length);
        Define.Weapons playerWeapon = (Define.Weapons)weaponNum;

        return playerWeapon;
    }

    public Define.Weapons SetRandomWeaponInItem()
    {
        int weaponNum = Random.Range(1, System.Enum.GetValues(typeof(Define.Weapons)).Length - System.Enum.GetValues(typeof(Define.PlayerStartWeapon)).Length);
        Define.Weapons playerWeapon = (Define.Weapons)weaponNum;
        return playerWeapon;
    }

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
            player.AddOrSetWeaponDict(Define.Weapons.Shotgun, 0);
        }
            
        else
        {
            Define.Weapons weaponType = (Define.Weapons)System.Enum.Parse(typeof(Define.Weapons), itemName);
            player.AddOrSetWeaponDict(weaponType, 1);
            Debug.Log($"weapon up! {player.GetWeaponDict()[weaponType]}");
        }
            

        Managers.UI.ClosePopupUI(Define.PopupUIGroup.UI_LevelUp);
    }

    public void ShowItemBoxUI()
    {
        Managers.UI.ShowPopupUI<UI_ItemBoxOpen>();
        Managers.GamePause();
    }
    public List<Define.Weapons> SetRandomWeaponfromItemBox()
    {
        int maxCount = 3;
        int rd = Random.Range(1, maxCount+1);
        int i = 0;
        List<Define.Weapons> weaponList = new List<Define.Weapons>();
        while(i < rd)
        {
            Define.Weapons wp = SetRandomWeaponInItem();
            if (weaponList.Contains(wp) || wp == Define.Weapons.Fireball)
                continue;
            weaponList.Add(wp);
            i++;
        }

        return weaponList;
    }

    public void SetLevelUpWeaponfromItemBox(List<Define.Weapons> weaponList)
    {
        PlayerStat player = Managers.Game.getPlayer().GetComponent<PlayerStat>();
        foreach(Define.Weapons weaponType in weaponList)
        {
            player.AddOrSetWeaponDict(weaponType, 1);
            Debug.Log($"weapon up! {player.GetWeaponDict()[weaponType]}");
        }
        
    }


}
