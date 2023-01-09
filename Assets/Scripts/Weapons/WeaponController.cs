using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    protected GameObject _player = null;
    PlayerStat _playerStat;
    Dictionary<int, Data.WeaponData> _weaponData;
    Dictionary<int, Data.WeaponLevelData> _weaponStat;
    public Define.WorldObject _type = Define.WorldObject.Weapon;
    Animator _anime;


    public abstract int _weaponType { get; }
    int _level = 1;
    public int Level 
    { 
        get { return _level; } 
        set 
        {
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
        Debug.Log($"player is null - {_player is null}");
        _playerStat = _player.GetComponent<PlayerStat>();
        Debug.Log($"playerStat - {_playerStat}");
        _weaponData = Managers.Data.WeaponData;
        Debug.Log($"WeaponData loaded!");
        _anime = transform.GetComponent<Animator>();
        _weaponStat = MakeLevelDataDict(_weaponType);

    }

    protected virtual void Spawn()
    {

    }

    protected virtual void SetWeaponStat()
    {
        if(_level > 5)
            _level = 5;

        _damage = _weaponStat[_level].damage * _playerStat.Damage;
        _movSpeed = _weaponStat[_level].movSpeed;
        _force = _weaponStat[_level].force;
        _cooldown = _weaponStat[_level].cooldown;
        _size = _weaponStat[_level].size;
        _penetrate = _weaponStat[_level].penetrate;
        _countPerCreate = _weaponStat[_level].countPerCreate;
    }

    protected Dictionary<int, Data.WeaponLevelData> MakeLevelDataDict(int weaponID)
    {
        Dictionary<int, Data.WeaponLevelData> _weaponLevelData = new Dictionary<int, Data.WeaponLevelData>();
        foreach (Data.WeaponLevelData weaponLevelData in _weaponData[weaponID].weaponLevelData)
            _weaponLevelData.Add(weaponLevelData.level, weaponLevelData);
        return _weaponLevelData;

    }

    

}
