using UnityEngine;

public class Knife : MonoBehaviour
{
    public Vector3 _dir = new Vector3(1, 0, 0);

    [SerializeField] 
    private float _lifeTime = 3f;
    [SerializeField] 
    private float _createTime = 0f;

    public int _damage = 10;
    public float _speed = 10f;
    public float _force = 0f;
    public int _panatrate = 1;
    private int _piercing = 0;


    private void OnEnable()
    {
        _createTime = Managers.GameTime;
    }

    void FixedUpdate()
    {
        if (Managers.GameTime - _createTime > _lifeTime)
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
            
            go.GetComponent<BaseController>().OnDamaged(_damage, _force);
            _piercing++;
            if (_piercing >= _panatrate)
                Managers.Resource.Destroy(gameObject);
        }
    }


    void OnMove()
    {
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle);
        transform.position += _dir * (_speed * Time.fixedDeltaTime);
    }
}