using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected float _Movespeed;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    

    public int Level { get { return _level; } set { _level = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int MaxHP { get { return _maxHp; } set { _maxHp = value; } }
    public float MoveSpeed { get { return _Movespeed; } set { _Movespeed = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }


}
