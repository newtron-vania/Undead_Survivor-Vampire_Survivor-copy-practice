using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

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
        _stat.MaxHP = SetRandomStat(data.maxHp);
        _stat.HP = _stat.MaxHP;
        _stat.Attack = SetRandomStat(data.attack);
        _stat.Defense = SetRandomStat(data.defense);
        _stat.Exp = SetRandomStat(data.exp);
    }
    
    int SetRandomStat(int value)
    {
        value = (int)(value * Random.Range(0.8f, 1.2f));
        return value;
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

            SpawnExp();
            Managers.Game.Despawn(gameObject);
        }
    }

    void SpawnExp()
    {
        GameObject expGo = Managers.Game.Spawn(Define.WorldObject.Unknown, "Content/Exp");
        ExpPoint expPoint = expGo.GetComponent<ExpPoint>();
        expPoint.Exp = _stat.Exp;
        expGo.transform.position = transform.position;
        if (expPoint.Exp < 5)
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[0];
        else if(expPoint.Exp<10)
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[1];
        else
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[2];

    }
}