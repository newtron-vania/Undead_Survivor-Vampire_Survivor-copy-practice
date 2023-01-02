using System.Collections.Generic;
using Data;
using UnityEngine;

public class SpinWeaponSpawner : WeaponSpawner
{
    Dictionary<int, WeaponLevelData> _spinWeaponStat;
    private List<GameObject> _spinWeapons = new List<GameObject>();
    [SerializeField] private int _objSize = 5;
    private float _circleR = 4;
    private float _deg = 0;
    [SerializeField] private float _objSpeed = 500;


    void Start()
    {
        _spinWeaponStat = MakeLevelDataDict(3);
        SetSpinStat(_level);
    }

    void Update()
    {
        SetSpinStat(_level);

        _deg += Time.deltaTime * _objSpeed;
        if (_deg < 360)
        {
            for (int i = 0; i < _objSize; i++)
            {
                var rad = Mathf.Deg2Rad * (_deg + (i * (360 / _objSize)));
                var x = _circleR * Mathf.Sin(rad);
                var y = _circleR * Mathf.Cos(rad);
                _spinWeapons[i].transform.position = transform.position + new Vector3(x, y);
                _spinWeapons[i].transform.rotation = Quaternion.Euler(0, 0, _deg * Mathf.Max(5f, _objSpeed/25));
            }
        }
        else
        {
            _deg = 0;
        }
    }

    //Todo
    protected override void SetWeaponStat(GameObject weapon)
    {
        SpinWeapon spin = weapon.GetComponent<SpinWeapon>();
        spin.damage = _spinWeaponStat[_level].damage;
    }

    void SetSpinStat(int level)
    {
        base.SetWeaponStat(gameObject);
        _objSize = _spinWeaponStat[level].createPerCount;
        _objSpeed = _spinWeaponStat[level].movSpeed;

        while(_spinWeapons.Count > _objSize)
        {
            GameObject spin = _spinWeapons[_spinWeapons.Count-1];
            Managers.Resource.Destroy(spin);
            _spinWeapons.RemoveAt(_spinWeapons.Count - 1);
        }

        while(_spinWeapons.Count < _objSize)
        {
            GameObject spinWeapon = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Spin");
            SetWeaponStat(spinWeapon);
            _spinWeapons.Add(spinWeapon);
        }
    }
}