using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileReRosition : MonoBehaviour
{
    [SerializeField] Vector2Int _tilePosition;


    private void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, _tilePosition);
    }
}
