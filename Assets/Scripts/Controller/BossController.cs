using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : BaseController
{
    public GameObject hudDamageText;

    protected EnemyStat _stat;
    public RuntimeAnimatorController[] animeCon;
    public Rigidbody2D _target;
    bool _isLive = true;
    bool _isAttack = false;
    public float skillcool = 8f;
    public int randStat = 0;
    bool useSkill = false;

    protected override void Init()
    {
        _stat = GetComponent<EnemyStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Enemy;
        _target = Managers.Game.getPlayer().GetComponent<Rigidbody2D>();
        _stat.MonsterStyle = Define.MonsterStyle.unknown;
        _stat.MonsterType = Define.MonsterType.Boss;
        
    }

    private void FixedUpdate()
    {
        if (!_isLive)
            return;
        Vector2 dirVec = _target.position - _rigid.position;
        if(!useSkill && dirVec.magnitude < 10)
        {
            float rd = Random.Range(0, 100f);

            if (rd < randStat)
            {
                Debug.Log("Play Skill1");
                Skill1();
            }

            else
            {
                Debug.Log("Play Skill2");
                Skill2();
            }
                
        }
        else if(useSkill && _isAttack)
        {
            _rigid.velocity = Vector2.zero;
        }
        else if (!_isAttack)
        {
            Vector2 nextVec = dirVec.normalized * (_stat.MoveSpeed * Time.fixedDeltaTime);
            _rigid.MovePosition(_rigid.position + nextVec);
            _rigid.velocity = Vector2.zero;
        }
    }

    public void SetExp(int level)
    {
        _stat.ExpPoint = 10 * level;
        _stat.ExpMul = 10;

    }

    private void LateUpdate()
    {
        _sprite.flipX = (_target.position.x - _rigid.position.x < 0) ? true : false;
    }

    private void OnEnable()
    {
        _isLive = true;
        _stat.HP = _stat.MaxHP;
    }



    void StartSkill1()
    {
        _anime.Play("Mushromm_Attack1", -1, 0);
    }
    void StartTimeStop()
    {
        Debug.Log("StartTimeStop!");

        StartCoroutine(TimePlay());
    }

    IEnumerator TimePlay()
    {
        UI_TimeStop TimeStopUI = Managers.UI.ShowPopupUI<UI_TimeStop>(name:null,true);
        TimeStopUI.gameObject.GetOrAddComponent<Canvas>().sortingOrder = 0;
        Managers.GamePause();
        float time = 0;
        while(time < 2f)
        {
            if(Managers.UI.GetPopupUICount() < 2)
            {
                time += Time.fixedDeltaTime;
            }
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }
        

        Debug.Log("Play!");
        Managers.UI.CloseAllGroupPopupUI(TimeStopUI._popupID);
        int[] num = new int[] { -1, 1 };
        int wRand = Random.Range(0, num.Length);
        int hRand = Random.Range(0, num.Length);

        _rigid.position = _target.position + new Vector2(num[wRand], num[hRand]);
        _rigid.velocity = Vector2.zero;
        _anime.Play("Mushromm_Run");

        yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);

        _isAttack = false;
        yield return new WaitForSeconds(skillcool);
        useSkill = false;
    }
    void Skill1()
    {
        useSkill = true;
        _isAttack = true;
        _anime.Play("Mushromm_AttackReady1", -1, 0);
    }


    void PlayRush()
    {
        Debug.Log("Rush!");
        _anime.SetBool("doRush", true);
        StartCoroutine(Rush());
    }

    IEnumerator Rush()
    {
        for(int i = 0; i<3; i++)
        {
            Vector3 targetPosition = _target.position;
            Debug.Log($"player Position : {targetPosition}");
            Vector3 dirVec = (targetPosition - transform.position).normalized;
            float time = 0;
            float RushSpeed = ((targetPosition + (dirVec * 2)) - transform.position).magnitude * Time.fixedDeltaTime;
            while (time < 1)
            {
                Vector2 nextVec = dirVec * RushSpeed;
                _rigid.MovePosition(_rigid.position + nextVec);
                _rigid.velocity = Vector2.zero;
                yield return new WaitForFixedUpdate();
                time += Time.fixedDeltaTime;
            }
            if(i < 2)
                yield return new WaitForSeconds(0.5f);
        }
        _anime.SetBool("doRush", false);
        _anime.Play("Mushromm_Run");
        _isAttack = false;
        yield return new WaitForSeconds(skillcool);
        useSkill = false;


    }

    void Skill2()
    {
        useSkill = true;
        _isAttack = true;
        _anime.Play("Mushromm_AttackReady2", -1, 0);
    }

    public override void OnDamaged(int damage, float force = 0)
    {
        Managers.Event.PlayHitEnemyEffectSound();
        int calculateDamage = Mathf.Max(damage - _stat.Defense, 1);
        _stat.HP -= calculateDamage;
        _rigid.AddForce((_rigid.position - _target.position).normalized * (force * 500f));
        FloatDamageText(calculateDamage);

        OnDead();
    }


    void FloatDamageText(int damage)
    {
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = transform.position + Vector3.up * 1.5f; // 표시될 위치
        hudText.GetComponent<UI_DamageText>().damage = damage; // 데미지 전달
    }

    public override void OnDead()
    {
        if (_stat.HP <= 0)
        {
            _isLive = false;
            _stat.HP = 0;

            SpawnExp();
            _anime.Play("Mushromm_Death");
            Managers.UI.ShowPopupUI<UI_GameVictory>();
            Managers.GamePause();
        }
    }

    void SpawnExp()
    {
        for(int i = 0; i<5; i++)
        {
            GameObject expGo = Managers.Game.Spawn(Define.WorldObject.Unknown, "Content/Exp");
            Exp_Item expPoint = expGo.GetComponent<Exp_Item>();
            expGo.GetComponent<SpriteRenderer>().sprite = expPoint._sprite[2];
            expPoint._exp = _stat.ExpPoint;
            expPoint._expMul = _stat.ExpMul;
            expGo.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }
}
