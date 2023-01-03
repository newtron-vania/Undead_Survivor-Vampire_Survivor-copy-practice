public class EnemyStat : Stat
{
    private int _expPoint;
    public int ExpPoint
    {
        get { return _expPoint;} set { _expPoint = value; }}
    void Start()
    {
        Level = 1;
        HP = 10;
        MaxHP = 10;
        MoveSpeed = 2.0f;
        Attack = 1;
        Defense = 0;
        ExpPoint = 5;
    }
}