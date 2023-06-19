using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LightningController : WeaponController
{
    public override int _weaponType { get { return (int)Define.Weapons.Lightning; } }


    private GameObject _playerUI = null;
    private Image _image_skill;

    private bool _isCool = false;
    protected override void SetWeaponStat()
    {
        base.SetWeaponStat();
    }

    private void OnEnable()
    {
        _playerUI = GameObject.Find("UI_Player");
        if (object.ReferenceEquals(_playerUI, null))
        {
            Managers.UI.ShowSceneUI<UI_Player>();
            return;
        }

        _image_skill = _playerUI.FindChild<Image>("CursorCoolTimeImg");
        _image_skill.gameObject.SetActive(true);
    }

    void Update()
    {
        UpdateSkillCoolTimeImage();
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isCool)
            {
                StartCoroutine(DamageCoolTime());
                StartCoroutine(LightnigEffect());
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Managers.Game.MousePos), _size, LayerMask.GetMask("Enemy"));
                foreach (Collider2D coll in collider2Ds)
                {
                    GameObject go = coll.gameObject;
                    go.GetComponent<BaseController>().OnDamaged(_damage, _force);
                }
            }
        }
    }

    void UpdateSkillCoolTimeImage()
    {
        _image_skill.transform.position = Managers.Game.MousePos;
    }


    IEnumerator DamageCoolTime()
    {
        _isCool = true;
        float currentCooltime = _cooldown;
        while (currentCooltime > 0f)
        {
            currentCooltime -= Time.deltaTime;
            _image_skill.fillAmount = ((_cooldown - currentCooltime) / _cooldown);
            yield return new WaitForFixedUpdate();

        }
        _isCool = false;
    }
    IEnumerator LightnigEffect()
    {
        GameObject lightnigEffect = Managers.Game.Spawn(Define.WorldObject.Unknown, "Weapon/Lightning");
        lightnigEffect.transform.position = Camera.main.ScreenToWorldPoint(Managers.Game.MousePos) - new Vector3(0,0, Camera.main.transform.position.z);
        yield return new WaitForSeconds(0.5f);
        Managers.Resource.Destroy(lightnigEffect);
    }
}
