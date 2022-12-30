using UnityEngine;

public class Knife : MonoBehaviour
{
    public Vector3 dir = new Vector3(1, 0, 0);
    public int damage = 10;
    public float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float createTime = 0f;


    private void OnEnable()
    {
        createTime = Managers.GameTime;
    }

    void Update()
    {
        if (Managers.GameTime - createTime > lifeTime)
        {
            Debug.Log("Knife lifetime is over!");
            Managers.Resource.Destroy(gameObject);
        }

        transform.position += dir * (speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.CompareTag("Enemy"))
        {
            EnemyStat stat = go.GetComponent<EnemyStat>();
            stat.HP -= Mathf.Max(damage - stat.Defense, 1);
            Managers.Resource.Destroy(gameObject);
            Debug.Log($"knife damaged to the enemy. enemy's hp is ${stat.HP}");

            stat.OnDead();
        }
    }
}