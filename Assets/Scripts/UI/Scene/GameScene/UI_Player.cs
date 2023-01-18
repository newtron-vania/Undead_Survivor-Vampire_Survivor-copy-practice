using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : UI_Scene
{
    enum Texts
    {
        GameTime,
        LevelText
    }

    enum Images
    {
        WeaponImgList,
        CursorCoolTimeImg
    }
    enum Sliders
    {
        ExpSlider
    }

    public override void Init()
    {
        base.Init();
        Managers.Sound.Play(Define.BGMs.BGM_01.ToString(), Define.Sound.Bgm);
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));
    }

    public void SetPlayerUI()
    {
        SetGameTime();
        SetExpAndLevel();
    }
    void SetGameTime()
    {
        Get<TextMeshProUGUI>((int)Texts.GameTime).text = string.Format("{0:D2}:{1:D2}", (int)Mathf.Floor(Managers.GameTime / 60),
            (int)Mathf.Floor(Managers.GameTime % 60));
    }

    void SetExpAndLevel()
    {
        PlayerStat player = Managers.Game.getPlayer().GetOrAddComponent<PlayerStat>();
        double ratio = player.Exp / (double)player.MaxExp;
        if (ratio < 0)
            ratio = 0;
        else if (ratio > 1)
            ratio = 1;
        Get<Slider>((int)Sliders.ExpSlider).value = (float)ratio;
        GetText((int)Texts.LevelText).text = player.Level.ToString();
    }

    public void SetWeaponImage(PlayerStat player)
    {
        GameObject weaponImgList = GetImage((int)Images.WeaponImgList).gameObject;

        foreach(Transform child in weaponImgList.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        foreach(KeyValuePair<Define.Weapons, int> weapon in player.GetWeaponDict())
        {
            Image weaponImg =  Managers.Resource.Instantiate("UI/SubItem/WeaponInven", weaponImgList.transform).GetOrAddComponent<Image>();
            weaponImg.gameObject.GetOrAddComponent<WeaponListImage>().SetInfo(weapon.Key.ToString());
            
        }

    }
    public void ActiveCheckCursorImage()
    {
        GameObject cursorCoolTimeImgGo = Get<Image>((int)Images.CursorCoolTimeImg).gameObject;

        cursorCoolTimeImgGo.SetActive(true);
    }
}
