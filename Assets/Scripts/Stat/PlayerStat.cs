using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{


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
