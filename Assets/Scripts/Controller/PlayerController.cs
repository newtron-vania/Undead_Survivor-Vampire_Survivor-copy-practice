using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    protected PlayerStat _stat;
    public RuntimeAnimatorController[] animeCon;
    [SerializeField] Vector2 _inputVec;
    [SerializeField] public Vector2 _lastDirVec = new Vector2(1, 0);
    bool _isDamaged = false;
    float _invincibility_time = 0.2f;

    Slider _slider;

    protected override void Init()
    {
        _stat = GetComponent<PlayerStat>();
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anime = GetComponent<Animator>();
        _type = Define.WorldObject.Player;
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform, "UI_HPBar");
    }
    

    void Update()
    {
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = _inputVec.normalized * (_stat.MoveSpeed * Time.fixedDeltaTime);
        //Position change the player to last direction;
        _rigid.MovePosition(_rigid.position + nextVec);

        if (_inputVec.normalized.magnitude != 0)
        {
            _lastDirVec = _inputVec.normalized;
        }
    }

    private void LateUpdate()
    {
        _anime.SetFloat("speed", _inputVec.magnitude);
        if (_inputVec.x != 0)
        {
            _sprite.flipX = (_inputVec.x < 0) ? true : false;
        }
    }
    public void Init(Data.Player playerData)
    {
        _anime.runtimeAnimatorController = animeCon[playerData.id - 1];
        _stat.MaxHP = playerData.maxHp;
        _stat.HP = playerData.maxHp;
        _stat.Damage = playerData.damage;
        _stat.Defense = playerData.defense;
        _stat.MoveSpeed = playerData.moveSpeed;
        _stat.Cooldown = playerData.coolDown;
        _stat.Amount = playerData.amount;
        _stat.AddOrSetWeaponDict((Define.Weapons)playerData.weaponID, 1);
    }
    public void OnDamaged(Collision2D collision)
    {
        _isDamaged = true;
        Stat EnemyStat = collision.transform.GetComponent<EnemyStat>();


        _stat.HP -= Mathf.Max(EnemyStat.Damage - _stat.Defense, 1);
        
        if (_stat.HP <= 0)
            OnDead();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            float currentTime = Managers.GameTime;
            if (!_isDamaged)
            {
                OnDamaged(collision);
                StartCoroutine(OnDamagedColor());
            }
        }
    }

    public override void OnDamaged(int damage, float force = 0)
    {
        Managers.Event.PlayHitPlayerEffectSound();
        _stat.HP -= Mathf.Max(damage - _stat.Defense, 1);
        OnDead();
    }

    IEnumerator OnDamagedColor()
    {
        _sprite.color = Color.red;

        yield return new WaitForSeconds(_invincibility_time);

        _isDamaged = false;
        _sprite.color = Color.white;
    }


    public override void OnDead()
    {
        if(_stat.HP < 0)
        {
            _anime.SetTrigger("dead");
            _stat.HP = 0;
            Managers.UI.ShowPopupUI<UI_GameOver>();
            Managers.GamePause();
        }
            
    }
    

}