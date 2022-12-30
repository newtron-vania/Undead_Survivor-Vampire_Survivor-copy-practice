public class EnemyStat : Stat
{
    void Awake()
    {
        Level = 1;
        HP = 10;
        MaxHP = 10;
        MoveSpeed = 2.0f;
        Attack = 1;
        Defense = 0;
    }

    public override void OnDead()
    {
        if (HP <= 0)
        {
            HP = 0;
            Managers.Resource.Destroy(gameObject);
        }
    }
}