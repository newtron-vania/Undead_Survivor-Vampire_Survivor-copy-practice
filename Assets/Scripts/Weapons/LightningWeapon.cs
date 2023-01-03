using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningWeapon : WeaponSpawner
{

    bool _isAttack = false;
    Vector3 mouse_pos;
    Image _image_skill;

    void Awake()
    {
        //Todo connect WeaponData
        _weaponID = 5;
        _image_skill = GameObject.FindWithTag("coolTimeImg").GetComponent<Image>();
    }

    void Update()
    {
        UpdateSkillCoolTimeImage();
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isAttack)
            {
                SetWeaponStat();
                StartCoroutine(DamageCoolTime());
                StartCoroutine(LightnigEffect());
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(mouse_pos), _size, LayerMask.GetMask("Enemy"));
                foreach (Collider2D coll in collider2Ds)
                {
                    GameObject go = coll.gameObject;
                    go.GetComponent<EnemyController>().OnDamaged(_damage);
                }
                Debug.Log($"Lightning Attack! Give {_damage} to Enemy {collider2Ds.Length}");
            }
        }
    }

    void SetWeaponStat()
    {
        base.SetWeaponStat();
    }

    void UpdateSkillCoolTimeImage()
    {
        mouse_pos = Input.mousePosition;
        _image_skill.transform.position = mouse_pos;
    }

    IEnumerator DamageCoolTime()
    {
        _isAttack = true;
        float currentCooltime = _cooldown;
        while (currentCooltime > 0f)
        {
            currentCooltime -= Time.deltaTime;
            _image_skill.fillAmount = ((_cooldown - currentCooltime) / _cooldown);
            yield return new WaitForFixedUpdate();

        }
        _isAttack = false;
    }
    IEnumerator LightnigEffect()
    {
        GameObject lightnigEffect = Managers.Game.Spawn(Define.WorldObject.Unknown, "Weapon/Lightning");
        lightnigEffect.transform.position = Camera.main.ScreenToWorldPoint(mouse_pos) - new Vector3(0,0, Camera.main.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        Managers.Resource.Destroy(lightnigEffect);
    }
}
