using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpgdPanel : UI_Base
{
    enum Texts
    {
        UpgdTitleText,
        UpgdDescText
    }

    enum Images
    {
        UpgdImg,
        UpgdDescPanel
    }
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Get<Image>((int)Images.UpgdDescPanel).gameObject.AddUIEvent(OnStatOrWeaponUp);
    }

    void OnStatOrWeaponUp(PointerEventData data)
    {
        TextMeshProUGUI title =  Get<TextMeshProUGUI>((int)Texts.UpgdTitleText);
        Debug.Log($"{title} select!");
        Managers.Event.LevelUpOverEvent();
    }

    public void SetInfo(string title, string desc)
    {
        Get<TextMeshProUGUI>((int)Texts.UpgdTitleText).text = title;
        Get<TextMeshProUGUI>((int)Texts.UpgdDescText).text = desc;
    }
}
