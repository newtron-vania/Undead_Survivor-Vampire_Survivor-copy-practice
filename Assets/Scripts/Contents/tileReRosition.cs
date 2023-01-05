using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileReRosition : MonoBehaviour
{
    [SerializeField] Vector2Int tilePosition;


    private void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);
    }
}
