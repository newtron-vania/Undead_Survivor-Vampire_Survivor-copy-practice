using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Stat _stat;
    protected Rigidbody2D _rigid;
    protected SpriteRenderer _sprite;
    public Animator _anime;
    public Define.WorldObject _type = Define.WorldObject.Unknown;

    public abstract void OnDead();

    public virtual void OnDamaged()
    {

    }
}
