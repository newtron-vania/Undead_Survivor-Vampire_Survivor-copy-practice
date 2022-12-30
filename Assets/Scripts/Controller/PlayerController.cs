using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    [SerializeField]
    Vector2 _inputVec;
    public Vector2 _lastDirVec;
    float _damagedTime = 0f;


    Slider _slider;


    private void Awake()
    {
        _stat = GetComponent<PlayerStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Player;
    }

    void Start()
    {
        
    }

    void Update()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        Vector2 nextVec = _inputVec.normalized * _stat.MoveSpeed * Time.fixedDeltaTime;
        //위치 이동
        _rigid.MovePosition(_rigid.position +  nextVec);

    }

    private void LateUpdate()
    {
        _anime.SetFloat("speed", _inputVec.magnitude);
        if (_inputVec.x != 0)
        {
            _sprite.flipX = (_inputVec.x < 0) ? true : false;
        }
    }

    private void OnDamaged(Collision2D collision)
    {
        _damagedTime = Time.deltaTime;
        Stat EnemyStat = collision.transform.GetComponent<EnemyStat>();
        _stat.HP -= Mathf.Max(EnemyStat.Attack - _stat.Defense, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if(Time.deltaTime - _damagedTime > 0.1f)
            {
                OnDamaged(collision);
            }
        }
    }
}
