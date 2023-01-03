using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLayer : MonoBehaviour
{
    void Start()
    {
        int layer = gameObject.layer;
        Debug.Log($"{layer}, {LayerMask.NameToLayer("ItemBox")}");
        if (layer == LayerMask.NameToLayer("ItemBox"))
            Debug.Log("is the same");
        else
            Debug.Log("is not same");
    }

    void Update()
    {
        
    }
}
