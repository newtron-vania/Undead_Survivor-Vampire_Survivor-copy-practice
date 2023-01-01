using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSpawner : MonoBehaviour
{
    protected GameObject _player;
    protected Dictionary<int, Data.WeaponData> _weaponData;
    Animator _anime;


    public int level = 1;
    void Awake()
    {
        _player = transform.parent.gameObject;
        _weaponData = Managers.Data.WeaponData;
        _anime = transform.GetComponent<Animator>();
    }

    protected virtual void Spawn()
    {

    }

    protected virtual void SetWeaponStat(GameObject weapon)
    {
        if(level > 5)
            level = 5;
    }

    protected Dictionary<int, Data.WeaponLevelData> MakeLevelDataDict(int weaponID)
    {
        Dictionary<int, Data.WeaponLevelData> _weaponLevelData = new Dictionary<int, Data.WeaponLevelData>();
        foreach (Data.WeaponLevelData weaponLevelData in _weaponData[weaponID].weaponLevelData)
            _weaponLevelData.Add(weaponLevelData.level, weaponLevelData);
        return _weaponLevelData;

    }

    

}
