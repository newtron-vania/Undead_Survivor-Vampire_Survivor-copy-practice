using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{


    //1 : knife, 2 : firebal, 3: spin, 4: posion 5: lightning 6: shotgun
    //WeaponDict - WeaponID : WeaponLevel
    Dictionary<string, int> _playerStat = new Dictionary<string, int>();
    [SerializeField]
    Dictionary<Define.Weapons, int> _weaponDict = new Dictionary<Define.Weapons, int>();
    public Define.Weapons playerStartWeapon;
    public Dictionary<Define.Weapons, int> GetWeaponDict()
    {
        return _weaponDict;
    }
    public bool AddOrSetWeaponDict(Define.Weapons key, int value, bool addOrSet = false)
    {
        if (_weaponDict.Count == 0)
        {
            playerStartWeapon = key;
        }
        if (!_weaponDict.ContainsKey(key))
        {
            if (_weaponDict.Count >= 4)
                return false;
            //key spawn
            _weaponDict.Add(key, 0);
        }

        if (addOrSet == false)
            _weaponDict[key] += value;
        else
            _weaponDict[key] = value;

        SetWeaponLevel();
        return true;
    }

    public override int Level { get { return base.Level; } 
        set 
        { 
            base.Level = value;
            if (!_playerStat.ContainsKey("Level"))
                _playerStat.Add("Level", _level);
            _playerStat["Level"] = _level;
        } 
    }
    public override int MaxHP
    {
        get { return base.MaxHP; }
        set
        {
            base.MaxHP = value;
            if (!_playerStat.ContainsKey("MaxHP"))
                _playerStat.Add("MaxHP", _maxHp);
            _playerStat["MaxHP"] = _maxHp;
        }
    }
    public override float MoveSpeed
    {
        get { return base.MoveSpeed; }
        set
        {
            base.MoveSpeed = value;
            if (!_playerStat.ContainsKey("MoveSpeed"))
                _playerStat.Add("MoveSpeed", (int)_Movespeed);
            _playerStat["MoveSpeed"] = (int)_Movespeed;
        }
    }
    public override int Damage 
    {
        get { return base.Damage; }
        set
        {
            base.Damage = value;
            if (!_playerStat.ContainsKey("Damage"))
                _playerStat.Add("Damage", _damage);
            _playerStat["Damage"] = _damage;
        }
    }
    public override int Defense
    {
        get { return base.Defense; }
        set
        {
            base.Defense = value;
            if (!_playerStat.ContainsKey("Defense"))
                _playerStat.Add("Defense", _defense);
            _playerStat["Defense"] = _defense;
        }
    }
    public int Cooldown { get { return _cooldown; } 
        set 
        { 
            _cooldown = value;
            if (!_playerStat.ContainsKey("Cooldown"))
                _playerStat.Add("Cooldown", _cooldown);
            _playerStat["Cooldown"] = _cooldown;
        } 
    }
    public int Amount { get { return _amount; }
        set 
        { 
            _amount = value;
            if (!_playerStat.ContainsKey("Amount"))
                _playerStat.Add("Amount", _amount);
            _playerStat["Amount"] = _amount;
        } 
    }

    private long _exp;
    public long Exp
    {
        get { return _exp;}
        set
        {
            _exp = value;
            while (_exp >= MaxExp)
            {
               OnLevelUp();
            }
        }
    }
    private long _maxExp = 1;

    public long MaxExp
    {
        get => _maxExp;
        set => _maxExp = value;
    }

    private int _cooldown;
    private int _amount;




    void Awake()
    {
        Init();
    }

    void Init()
    {
        Level = 1;
        HP = 50;
        MaxHP = 50;
        MoveSpeed = 5.0f;
        Damage = 1;
        Defense = 0;
        Cooldown = 0;
        Amount = 0;
        Exp = 0;
        MaxExp = 10;
        SetWeaponLevel();
    }


    void OnLevelUp()
    {
        Managers.Event.LevelUpEvent();

        Level += 1;
        Exp = 0;
        MaxExp += Math.Max(100, (long)(_maxExp*1.1));
    }

    void SetWeaponLevel()
    {
        //Set CommonWeaponSpawingPool 
        foreach (KeyValuePair<Define.Weapons, int> weapon in GetWeaponDict())
        {
            string weaponName = weapon.Key.ToString();
            string weaponSpawningPool = weapon.Key + "SpawningPool";

            GameObject weaponPool = Util.FindChild(gameObject, weaponSpawningPool, true);
            if (weaponPool == null)
            {
                weaponPool = Managers.Resource.Instantiate($"Weapon/SpawningPool/{weaponSpawningPool}", transform);
            }
            if (weapon.Value == 0)
            {
                GetWeaponDict().Remove(weapon.Key);
                Managers.Resource.Destroy(weaponPool);
            }

            weaponPool.GetComponentInChildren<WeaponController>().Level = weapon.Value;
            Managers.UI.getSceneUI().GetComponent<UI_Player>().SetWeaponImage(Managers.Game.getPlayer().GetComponent<PlayerStat>());
        }
    }


    public Dictionary<string, int> getPlayerStatData()
    {
        return _playerStat;
    }
}
