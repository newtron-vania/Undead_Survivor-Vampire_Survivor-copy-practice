using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    public RuntimeAnimatorController[] animeCon;
    public Rigidbody2D _target;
    bool _isLive = true;


    private void Awake()
    {
        _stat = GetComponent<EnemyStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Enemy;
    }

    private void FixedUpdate()
    {
        if (!_isLive)
            return;

        Vector2 dirVec = _target.position - _rigid.position;
        Vector2 nextVec = dirVec.normalized * (_stat.MoveSpeed * Time.fixedDeltaTime);

        _rigid.MovePosition(_rigid.position + nextVec);
        _rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        _sprite.flipX = (_target.position.x - _rigid.position.x < 0) ? true : false;
    }

    private void OnEnable()
    {
        _target = Managers.Instance._player.GetComponent<Rigidbody2D>();
        _isLive = true;
        _stat.HP = _stat.MaxHP;
    }

    public void Init(SpawnData data)
    {
        _anime.runtimeAnimatorController = animeCon[data.spriteType];
        _stat.MoveSpeed = data.speed;
        _stat.MaxHP = data.maxHp;
        _stat.HP = _stat.MaxHP;
    }

    public override void OnDamaged(int damage)
    {
        _stat.HP -= Mathf.Max(damage - _stat.Defense, 1);
        OnDead();
    }
    public override void OnDead()
    {
        if(_stat.HP <= 0)
        {
            _isLive = false;
            _stat.HP = 0;
            Managers.Game.Despawn(gameObject);
        }
    }

}