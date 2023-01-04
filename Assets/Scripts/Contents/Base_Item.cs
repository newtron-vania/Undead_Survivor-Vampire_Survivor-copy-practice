using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Base_Item : MonoBehaviour
{
   public Transform target { get; set; } = null;

   public abstract void OnItemEvent(PlayerStat player);

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.transform.tag == "Player")
      {
         PlayerStat playerStat = col.GetComponent<PlayerStat>();
         target = null;
         OnItemEvent(playerStat);
         Managers.Resource.Destroy(gameObject);
      }
   }
}
