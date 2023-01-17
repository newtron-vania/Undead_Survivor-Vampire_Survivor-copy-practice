using System.Collections;
using UnityEngine;

public class EnemyController : BaseController
{
    public GameObject hudDamageText;
    
    protected EnemyStat _stat;
    public RuntimeAnimatorController[] animeCon;
    public Rigidbody2D _target;
    bool _isLive = true;


    protected override void Init()
    {
        _stat = GetComponent<EnemyStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Enemy;
        _target = Managers.Game.getPlayer().GetComponent<Rigidbody2D>();
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
        _target = Managers.Game.getPlayer().GetComponent<Rigidbody2D>();
        _isLive = true;
        _stat.HP = _stat.MaxHP;
    }

    public void Init(Data.Monster monsterStat, int level, Define.MonsterType type)
    {
        int mul = 1;
        switch (type)
        {
            case Define.MonsterType.Enemy:
                mul = 1;
                break;
            case Define.MonsterType.middleBoss:
                mul = 20;
                break;
        }
        _anime.runtimeAnimatorController = animeCon[monsterStat.id-1];
        _stat.MonsterStyle = (Define.MonsterStyle)System.Enum.Parse(typeof(Define.MonsterStyle), monsterStat.name);
        _stat.MonsterType = type;
        _stat.MoveSpeed = monsterStat.moveSpeed *((float)(100f+ level)/100f) * (mul*0.05f);
        _stat.MaxHP = SetRandomStat((int)(monsterStat.maxHp * ((100f + 10f*level)/ 100f))) * mul;
        _stat.HP = _stat.MaxHP;
        _stat.Damage = SetRandomStat((int)(monsterStat.damage * ((100f + level) / 100f)))* mul;
        _stat.Defense = SetRandomStat((int)(monsterStat.defense * ((100f + level) / 100f))) * mul;
        _stat.ExpPoint = 5*level;
        _stat.ExpMul = monsterStat.expMul;
    }
    
    int SetRandomStat(int value)
    {
        value = (int)(value * Random.Range(0.9f, 1.1f));
        return value;
    }

    
    
    public override void OnDamaged(int damage, float force = 0)
    {
        Managers.Event.PlayHitEnemyEffectSound();
        _anime.SetTrigger("Hit");
        int calculateDamage = Mathf.Max(damage - _stat.Defense, 1);
        _stat.HP -= calculateDamage;
        _rigid.AddForce((_rigid.position - _target.position).normalized * (force * 500f));
        FloatDamageText(calculateDamage);

        OnDead();
    }


    void FloatDamageText(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = transform.position + Vector3.up*1.5f; // 표시될 위치
        hudText.GetComponent<UI_DamageText>().damage = damage; // 데미지 전달
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
        Exp_Item expPoint = expGo.GetComponent<Exp_Item>();
        expPoint._exp = _stat.ExpPoint;
        expPoint._expMul = _stat.ExpMul;
        expGo.transform.position = transform.position;
        if (expPoint._expMul == 1)
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[0];
        else if(expPoint._expMul == 2)
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[1];
        else
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[2];

    }

    void DropItem()
    {
        GameObject item = null;
        float rand = Random.Range(0, 100);
        if(rand < 3)
        {
            item = Managers.Resource.Instantiate("Content/ItemBox");
        }
        else if(rand < 8)
        {
            int rd = Random.Range(1, 11);
            if(rd < 7)
            {
                item = Managers.Resource.Instantiate("Content/Health");
            }
            else
            {
                item = Managers.Resource.Instantiate("Content/Magnet");
            }
        }
        if (item == null)
            return;
        item.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
    }
}