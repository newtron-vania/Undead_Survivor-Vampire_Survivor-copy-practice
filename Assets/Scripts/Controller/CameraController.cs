using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject _player = null;

    Vector3 _delta = new Vector3(0, 0, -10f);

    public void SetPlayer(GameObject player) { _player = player; }
    void Update()
    {
        if (object.ReferenceEquals(_player, null))
            return;

        transform.position = Vector3.Slerp(transform.position, _player.transform.position + _delta, 1.0f);
    }
}
