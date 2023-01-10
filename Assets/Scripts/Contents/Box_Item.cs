using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box_Item : Base_Item
{
    public override void OnItemEvent(PlayerStat player)
    {

        Managers.Event.ShowItemBoxUI();
        Managers.Resource.Destroy(gameObject);
    }
}
