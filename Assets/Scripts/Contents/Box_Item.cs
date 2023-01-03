using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box_Item : Base_Item
{
    public override void OnItemEvent(PlayerStat player)
    {
        //Weapon LevelUp Event
        
        //
        Debug.Log("ItemBox get!");
        Managers.Resource.Destroy(gameObject);
    }
}
