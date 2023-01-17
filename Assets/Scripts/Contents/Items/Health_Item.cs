using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Item : Base_Item
{
    int healthHp = 30;
    public override void OnItemEvent(PlayerStat player)
    {
        player.HP += healthHp;
    }

}
