using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    protected GameObject _player = null;
    private PlayerStat _playerStat;
    private Dictionary<int, Data.WeaponData> _weaponData;
    private Dictionary<int, Data.WeaponLevelData> _weaponStat;
    public Define.WorldObject _type = Define.WorldObject.Weapon;
    private Animator _anime;


    public abstract int _weaponType { get; }
    private int _level = 1;
    public int Level 
    { 
        get 
        {
            Debug.Log($"Get level : {_level}");
            return _level; 
        } 
        set 
        {
            Debug.Log($"Set level : {_level}");
            _level = value;
            SetWeaponStat();
        } 
    }

    public int _damage=1;
    public float _movSpeed=1;
    public float _force=1;
    public float _cooldown=1;
    public float _size=1;
    public int _penetrate=1;
    public int _countPerCreate=1;


    
    void Awake()
    {
        _player = Managers.Game.getPlayer();
        _playerStat = _player.GetComponent<PlayerStat>();
        _weaponData = Managers.Data.WeaponData;
        _anime = transform.GetComponent<Animator>();
        _weaponStat = MakeLevelDataDict(_weaponType);

    }

    protected virtual void SetWeaponStat()
    {
        if (_level > 5)
            _level = 5;

        _damage = (int)(_weaponStat[_level].damage * ((float)(100+ _playerStat.Damage)/100f));
        _movSpeed = _weaponStat[_level].movSpeed;
        _force = _weaponStat[_level].force;
        _cooldown = _weaponStat[_level].cooldown * (100f/(100f +_playerStat.Cooldown));
        _size = _weaponStat[_level].size;
        _penetrate = _weaponStat[_level].penetrate;
        _countPerCreate = _weaponStat[_level].countPerCreate + _playerStat.Amount;
    }

    protected Dictionary<int, Data.WeaponLevelData> MakeLevelDataDict(int weaponID)
    {
        Dictionary<int, Data.WeaponLevelData> _weaponLevelData = new Dictionary<int, Data.WeaponLevelData>();
        foreach (Data.WeaponLevelData weaponLevelData in _weaponData[weaponID].weaponLevelData)
            _weaponLevelData.Add(weaponLevelData.level, weaponLevelData);
        return _weaponLevelData;

    }

    

}
