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
        Managers.Sound.Play("GetMagnet");
        ItemGetter itemGetter = player.transform.Find("GetItemRange").GetComponent<ItemGetter>();
        float size = itemGetter._size;
        itemGetter.transform.GetComponent<CircleCollider2D>().radius = size * 100;
        Debug.Log($"Magnet Get! {size * 100}");
        yield return new WaitForFixedUpdate();
        itemGetter.transform.GetComponent<CircleCollider2D>().radius = size;
        Debug.Log($"Range restore! : {size}");

    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            PlayerStat playerStat = col.GetComponent<PlayerStat>();
            OnItemEvent(playerStat);
            Managers.Resource.Destroy(gameObject, 0.3f);
        }
    }
}
