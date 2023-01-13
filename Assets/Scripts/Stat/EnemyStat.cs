public class EnemyStat : Stat
{
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