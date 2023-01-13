using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Item : Base_Item
{
    public Sprite[] _sprite;
    public long _exp;
    public int _expMul;

    public override void OnItemEvent(PlayerStat player)
    {
        long percentExp = (long)Math.Truncate((double)player.MaxExp / 100);
        if (percentExp > _exp)
            _exp = percentExp;
        player.Exp += _exp*_expMul;
        Managers.Resource.Destroy(gameObject);
    }

}
