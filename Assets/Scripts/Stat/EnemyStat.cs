public class EnemyStat : Stat
{
    public Define.MonsterStyle MonsterStyle { get; set; } = Define.MonsterStyle.unknown;
    public Define.MonsterType MonsterType { get; set; } = Define.MonsterType.Enemy;
    private long _expPoint;
    public long ExpPoint
    {
        get { return _expPoint;} set { _expPoint = value; }}
    private int _expMul;
    public int ExpMul { get { return _expMul; } set { _expMul = value; } }

    void Start()
    {
        ExpPoint = 5;
        ExpMul = 1;
    }


}