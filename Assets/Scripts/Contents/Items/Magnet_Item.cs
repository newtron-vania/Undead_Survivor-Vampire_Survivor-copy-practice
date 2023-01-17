using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet_Item : Base_Item
{
    public override void OnItemEvent(PlayerStat player)
    {
        StartCoroutine(MagnetExp(player));
    }

    IEnumerator MagnetExp(PlayerStat player)
    {
        Transform go = player.transform.Find("GetItemRange");
        Vector3 itemRange = go.localScale;
        go.localScale *= 100;
        yield return new WaitForSeconds(0.1f);
        go.localScale = itemRange;
        
    }
}
