using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Vector2 MousePos;
    void Awake()
    {
        MousePos = Input.mousePosition;
    }

    void Update()
    {
        
    }
}
