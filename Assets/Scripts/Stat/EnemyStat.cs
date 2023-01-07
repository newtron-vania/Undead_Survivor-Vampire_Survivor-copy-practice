public class EnemyStat : Stat
{
    private int _expPoint;
    public int ExpPoint
    {
        get { return _expPoint;} set { _expPoint = value; }}
    void Start()
    {
        ExpPoint = 5;
    }
}