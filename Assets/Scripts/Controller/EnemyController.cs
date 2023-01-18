using System.Collections;
using UnityEngine;

public class EnemyController : BaseController
{
    public GameObject hudDamageText;
    
    protected EnemyStat _stat;
    public RuntimeAnimatorController[] animeCon;
    public Rigidbody2D _target;
    bool _isLive = true;
    bool _isRange = false;
    bool _isAttack = false;


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

        OnMove();

        if (_isRange && !_isAttack)
        {
            StartCoroutine(RangeAttack());
        }
    }

    void OnMove()
    {
        Vector2 dirVec = _target.position - _rigid.position;
        Vector2 nextVec = dirVec.normalized * (_stat.MoveSpeed * Time.fixedDeltaTime);

        _rigid.MovePosition(_rigid.position + nextVec);
        _rigid.velocity = Vector2.zero;
    }

    IEnumerator RangeAttack()
    {
        _isAttack = true;
        SpawnBullet();
        yield return new WaitForSeconds(2f);
        _isAttack = false;
    }

    void SpawnBullet()
    {
        EnemyBullet bullet = Managers.Resource.Instantiate("Weapon/EnemyBullet",_rigid.position).GetOrAddComponent<EnemyBullet>();
        bullet.damage = _stat.Damage;
        bullet.speed = 5f;
        bullet.dir = (_target.position - _rigid.position).normalized;
    }
    private void LateUpdate()
    {
        _sprite.flipX = (_target.position.x - _rigid.position.x < 0) ? true : false;
    }

    private void OnEnable()
    {
        _isLive = true;
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
                mul = 50;
                break;
        }
        _anime.runtimeAnimatorController = animeCon[monsterStat.id-1];
        if (monsterStat.id == 5)
            _isRange = true;
        if (type == Define.MonsterType.middleBoss)
            transform.localScale = Vector3.one * 2;
        _stat.MonsterStyle = (Define.MonsterStyle)System.Enum.Parse(typeof(Define.MonsterStyle), monsterStat.name);
        _stat.MonsterType = type;
        _stat.MoveSpeed = monsterStat.moveSpeed *((float)(100f+ level)/100f);
        _stat.MaxHP = SetRandomStat((int)(monsterStat.maxHp * ((100f + 10f*level)/ 100f))) * mul;
        _stat.HP = _stat.MaxHP;
        _stat.Damage = SetRandomStat((int)(monsterStat.damage * ((100f + level) / 100f)));
        _stat.Defense = SetRandomStat((int)(monsterStat.defense * ((100f + level) / 100f)));
        _rigid.mass = 3;
        _stat.ExpPoint = 10*level;
        _stat.ExpMul = monsterStat.expMul;
        if(type == Define.MonsterType.middleBoss)
        {
            Debug.Log("Boss Spawn! ");
            Debug.Log($"MaxHp : {_stat.MaxHP}");
            Debug.Log($"Hp : {_stat.HP}");
            Debug.Log($"Damage : {_stat.Damage}");
            Debug.Log($"Defense : {_stat.Defense}");
            Debug.Log($"Weight : {_rigid.mass}");
        }
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
        _rigid.AddForce((_rigid.position - _target.position).normalized * (force * 200f));
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
            Managers.Event.DropItem(_stat,transform);
            transform.localScale = Vector3.one;
            Managers.Game.Despawn(gameObject);
        }
    }

    void SpawnExp()
    {
        GameObject expGo = Managers.Game.Spawn(Define.WorldObject.Unknown, "Content/Exp");
        expGo.transform.position = transform.position;
        Exp_Item expPoint = expGo.GetComponent<Exp_Item>();
        expPoint._exp = _stat.ExpPoint;
        expPoint._expMul = _stat.ExpMul;
        
        if (expPoint._expMul == 1)
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[0];
        else if(expPoint._expMul == 2)
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[1];
        else
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[2];

    }


}