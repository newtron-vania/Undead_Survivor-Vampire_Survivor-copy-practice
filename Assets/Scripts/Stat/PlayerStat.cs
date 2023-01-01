using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    //1 : knife, 2 : firebal, 3: spin, 4: posion
    private List<int> _weaponList = new List<int>();

    void Awake()
    {
        Level = 1;
        HP = 30;
        MaxHP = 30;
        MoveSpeed = 5.0f;
        Attack = 1;
        Defense = 0;
    }

}
