using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigid;
    protected SpriteRenderer _sprite;
    public Animator _anime;
    public Define.WorldObject _type = Define.WorldObject.Unknown;

    private void Awake()
    {
        Init();
    }

    public abstract void OnDead();

    public abstract void OnDamaged(int damage, float force = 0);

    protected abstract void Init();
}
