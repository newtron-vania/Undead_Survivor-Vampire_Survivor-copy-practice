using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int _damage = 10;
    public float _force = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<BaseController>().OnDamaged(_damage, _force);
        }
    }

    public void DoDestroy()
    {
        Managers.Resource.Destroy(transform.parent.gameObject);
    }
}
