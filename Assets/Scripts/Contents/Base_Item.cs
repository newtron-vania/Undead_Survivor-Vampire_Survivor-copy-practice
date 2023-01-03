using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Base_Item : MonoBehaviour
{
   private Transform _target = null;
   private float _movSpeed = 10f;

   private void FixedUpdate()
   {
      TraceTarget();
   }

   public abstract void OnItemEvent(PlayerStat player);
   
   public void SetTarget(Transform target)
   {
      _target = target;
   }

   void TraceTarget()
   {
      if (object.ReferenceEquals(_target,null))
         return;
      transform.position =
         Vector3.MoveTowards(transform.position, _target.transform.position, _movSpeed * Time.fixedDeltaTime);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.transform.tag == "Player")
      {
         PlayerStat playerStat = col.GetComponent<PlayerStat>();
         OnItemEvent(playerStat);
         Managers.Resource.Destroy(gameObject);
      }
   }
}
