using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    //1 : knife, 2 : firebal, 3: spin, 4: posion 5: lightning 6: shotgun
    public Dictionary<int, int> WeaponDict { get; set; } = new Dictionary<int, int>();
    private int _exp;

    private bool isLevelUp = false;
    public int Exp
    {
        get { return _exp;}
        set
        {
            _exp = value;
            while (_exp >= MaxExp)
            {
                //Managers.Event.LevelUpEvent();
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
    }
}
