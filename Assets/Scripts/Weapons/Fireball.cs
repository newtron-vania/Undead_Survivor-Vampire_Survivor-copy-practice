using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public Vector3 dirVec = new Vector3(1, 0, 0);

    public int damage = 10;
    public float speed = 3f;
    public float force = 0f;
    public float size = 1f;
    public int panatrate = 1;
    private int piercing = 0;

    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float createTime = 0f;

    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }


    void FixedUpdate()
    {
        if (Managers.GameTime - createTime > lifeTime)
        {
            Managers.Resource.Destroy(gameObject);
        }
        OnMove();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            piercing++;
            if(piercing >= panatrate)
            {
                GameObject explosion = Managers.Resource.Instantiate("Weapon/Explosion");
                Explosion explosionStat = explosion.GetComponent<Explosion>();
                explosionStat.damage = (int)(damage * 1.1);
                explosionStat.force = force;
                explosion.transform.localScale = Vector3.one * size;
                explosion.transform.position = transform.position;
                Managers.Resource.Destroy(gameObject);
            }
            else
                go.GetComponent<EnemyController>().OnDamaged(damage, force/2);
        }
    }


    void OnMove()
    {
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        transform.position += dirVec * (speed * Time.fixedDeltaTime);
    }
}
