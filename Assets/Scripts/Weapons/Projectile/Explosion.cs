using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage = 10;
    public float force = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<BaseController>().OnDamaged(damage, force);
        }
    }

    public void DoDestroy()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
