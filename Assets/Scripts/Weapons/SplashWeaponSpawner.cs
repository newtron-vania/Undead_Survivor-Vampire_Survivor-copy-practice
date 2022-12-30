using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashWeaponSpawner : WeaponSpawner
{
    List<Data.WeaponLevelData> _splashWeaponStat;
    // Start is called before the first frame update
    void Start()
    {
        _splashWeaponStat = weaponData[3].weaponLevelData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
