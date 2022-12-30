using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    protected GameObject _player;
    protected Dictionary<int, Data.WeaponData> weaponData;
    Animator _anime;

    [SerializeField]
    protected int level = 1;
    void Awake()
    {
        _player = transform.parent.gameObject;
        weaponData = Managers.Data.WeaponData;
        _anime = transform.GetComponent<Animator>();
    }

    protected virtual void Spawn()
    {

    }

    protected virtual void SetWeaponStat(List<Data.WeaponLevelData> weapon)
    {

    }

}
