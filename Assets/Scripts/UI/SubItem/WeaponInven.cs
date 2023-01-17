using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponInven : UI_Base
{
    enum Images
    {
        WeaponInven,
        WeaponImg
    }

    enum Texts
    {
        WeaponLevelText,
        LvText
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public void SetInfo(string weaponName, int weaponLevel)
    {
        GetImage((int)Images.WeaponImg).sprite = Managers.Resource.LoadSprite(weaponName);
        GetText((int)Texts.WeaponLevelText).text = weaponLevel.ToString();
    }
}
