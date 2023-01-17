using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGetter : MonoBehaviour
{
    public float _size;
    private float _movSpeed = 10f;
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
            StartCoroutine(GetItemInField(other.gameObject));
        }  
    }

    IEnumerator GetItemInField(GameObject item)
    {
        if (item.GetOrAddComponent<Base_Item>().target != _player)
        {
            item.GetOrAddComponent<Base_Item>().target = _player;
            while (item != null && item.activeSelf)
            {
                item.transform.position =
                    Vector3.MoveTowards(item.transform.position, _player.transform.position, _movSpeed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }
    }
}
