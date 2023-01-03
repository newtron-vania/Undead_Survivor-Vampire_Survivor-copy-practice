using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGetter : MonoBehaviour
{
    public float _size;
    private Transform _player;

    private void Awake()
    {
        transform.GetComponent<CircleCollider2D>().radius = _size;
        _player = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("ItemBox"))
                return;
            GetItemInField(other.gameObject);
        }  
    }

    void GetItemInField(GameObject item)
    {
        item.GetComponent<Base_Item>().SetTarget(_player);
    }
}
