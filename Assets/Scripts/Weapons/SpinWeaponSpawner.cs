using System.Collections.Generic;
using Data;
using UnityEngine;

public class SpinWeaponSpawner : WeaponSpawner
{
    List<WeaponLevelData> _spinWeaponStat;
    private List<SpinWeapon> _spinWeapons = new List<SpinWeapon>();
    private int _objSize = 5;
    private float _circleR = 4;
    private float _deg;
    private float _objSpeed = 500;

    // Start is called before the first frame update
    void Start()
    {
        _spinWeaponStat = weaponData[3].weaponLevelData;

        for (int i = 0; i < _objSize; i++)
        {
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Spin");
            SpinWeapon spinWeapon = go.GetComponent<SpinWeapon>();

            var rad = Mathf.Deg2Rad * (_deg + (i * (360 / _objSize)));
            var x = _circleR * Mathf.Sin(rad);
            var y = _circleR * Mathf.Cos(rad);

            spinWeapon.transform.position = transform.position + new Vector3(x, y);
            spinWeapon.transform.rotation =
                Quaternion.Euler(
                    0,
                    0,
                    (_deg + (i * (360 / _objSize))) * -1);

            _spinWeapons.Add(spinWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _deg += Time.deltaTime * _objSpeed;
        if (_deg < 360)
        {
            for (int i = 0; i < _objSize; i++)
            {
                var rad = Mathf.Deg2Rad * (_deg + (i * (360 / _objSize)));
                var x = _circleR * Mathf.Sin(rad);
                var y = _circleR * Mathf.Cos(rad);
                _spinWeapons[i].transform.position = transform.position + new Vector3(x, y);
                _spinWeapons[i].transform.rotation = Quaternion.Euler(
                    _player.transform.position.x,
                    _player.transform.position.y,
                    (_deg + (i * (360 / _objSize))) * -1);
            }
        }
        else
        {
            _deg = 0;
        }
    }
}