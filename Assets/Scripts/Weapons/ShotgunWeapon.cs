using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;

public class ShotgunWeapon : WeaponSpawner
{
    [SerializeField] private Vector3 holeVec;
    [SerializeField] private GameObject hand;

    private bool isShot = false;
    private float _bulletRange = 80f;
    
    void Awake()
    {
        _weaponID = 6;
    }

    void Update()
    {
        hand.transform.LookAt(Managers.Game.MousePos);
        if (!isShot)
        {
            StartCoroutine(ShotCoolTime());
        }
    }

    protected void SetWeaponStat()
    {
        base.SetWeaponStat();
        float bulletAngle = (_bulletRange/2) / Mathf.CeilToInt((_countPerCreate+1)/2);
        float angle = SetAngleFromHandToCursor();
        
        for (int i = 0; i < _countPerCreate; i++)
        {
            GameObject bullet = Managers.Game.Spawn(Define.WorldObject.Unknown, "Weapon/Bullet");
            bullet.transform.position = holeVec;
            //set damage, dir 
        }
        
    }

    float SetAngleFromHandToCursor()
    {
        Vector3 dirVec = (Managers.Game.MousePos - holeVec).normalized;
        return Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
    }


    IEnumerator ShotCoolTime()
    {
        isShot = true;
        


        yield return new WaitForSeconds(_cooldown);

        isShot = false;
    }
}
