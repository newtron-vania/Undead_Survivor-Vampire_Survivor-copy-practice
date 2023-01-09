using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    //1 : knife, 2 : firebal, 3: spin, 4: posion 5: lightning 6: shotgun
    //WeaponDict - WeaponID : WeaponLevel
    [SerializeField]
    Dictionary<Define.Weapons, int> _weaponDict = new Dictionary<Define.Weapons, int>();


    public Dictionary<Define.Weapons, int> GetWeaponDict()
    {
        return _weaponDict;
    }
    public void AddOrSetWeaponDict(Define.Weapons key, int value, bool addOrSet = false)
    {
        if (!_weaponDict.ContainsKey(key))
        {
            //key spawn
            _weaponDict.Add(key, 0);
        }

        if (addOrSet == false)
            _weaponDict[key] += value;
        else
            _weaponDict[key] = value;

        SetWeaponLevel();
    }


    private int _exp;

    public int Exp
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
    private int _maxExp = 1;

    public int MaxExp
    {
        get => _maxExp;
        set => _maxExp = value;
    }

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
        Exp = 0;
        MaxExp = 10;
        SetWeaponLevel();
    }


    void OnLevelUp()
    {
        Managers.Event.LevelUpEvent();

        Level += 1;
        Exp -= MaxExp;
        MaxExp *= 2;
    }

    public void SetWeaponLevel()
    {
        //Set WeaponSpawingPool 
        foreach (KeyValuePair<Define.Weapons, int> weapon in GetWeaponDict())
        {
            string weaponName = weapon.Key.ToString();
            string weaponSpawningPool = weapon.Key + "SpawningPool";

            GameObject weaponPool = Util.FindChild(gameObject, weaponSpawningPool);
            if (weaponPool == null)
            {
                weaponPool = Managers.Resource.Instantiate($"Weapon/SpawningPool/{weaponSpawningPool}", transform);
            }
            if (weapon.Value == 0)
            {
                Managers.Resource.Destroy(weaponPool);
            }

            Debug.Log($"Weapon Name : {weaponName}");
            Debug.Log($"currentLevel = {weaponPool.GetComponent<WeaponController>().Level}");
            Debug.Log($"ChangeLevel : {weapon.Value}");
            weaponPool.GetComponent<WeaponController>().Level = weapon.Value;
            Debug.Log($"Weapon Name : {weaponName}");
            Debug.Log($"currentLevel = {weaponPool.GetComponent<WeaponController>().Level}");
            Debug.Log($"ChangeLevel : {weapon.Value}");
        }
    }
}
