using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Item : Base_Item
{
    public Sprite[] _sprite;
    public int _exp;

    public override void OnItemEvent(PlayerStat player)
    {
        
        player.Exp += _exp;
        Managers.Resource.Destroy(gameObject);
    }

}
