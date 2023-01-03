using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (other.tag == "Player")
        {
            //Event
            
            //Destory
            Managers.Resource.Destroy(gameObject);
        }
    }
}
