using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    //1 : knife, 2 : firebal, 3: spin, 4: posion
    private List<int> _weaponList = new List<int>();
    private int _maxExp;

    public int MaxExp
    {
        get => _maxExp;
        set => _maxExp = value;
    }

    void Awake()
    {
        Level = 1;
        HP = 50;
        MaxHP = 50;
        MoveSpeed = 5.0f;
        Attack = 1;
        Defense = 0;
        Exp = 0;
        MaxExp = 1000;
    }

}
