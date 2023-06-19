using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_PlayerStat : UI_Popup
{
    enum Objects
    {

        StatPanel,
        WeaponPanel
    }
    enum Images
    {
        BackgroundImg,
    }

    private PlayerStat _player;

    public override Define.PopupUIGroup PopupID { get { return Define.PopupUIGroup.UI_GameMenu; } }


    public override void Init()
    {
        base.Init();
        _player = Managers.Game.getPlayer().GetOrAddComponent<PlayerStat>();
        Bind<GameObject>(typeof(Objects));
        Bind<Image>(typeof(Images));

        GameObject statPanel = GetObject((int)Objects.StatPanel);
        foreach(Transform child in statPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        foreach(KeyValuePair<string, int> stat in _player.getPlayerStatData())
        {

            StatInven statInven = Managers.UI.MakeSubItem<StatInven>(statPanel.transform, "StatData");
            statInven.SetInfo(stat.Key, stat.Value);
        }

        GameObject weaponPanel = GetObject((int)Objects.WeaponPanel);
        foreach (Transform child in weaponPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
        foreach (KeyValuePair<Define.Weapons, int> weapon in _player.GetWeaponDict())
        {
            WeaponInven weaponInven = Managers.UI.MakeSubItem<WeaponInven>(weaponPanel.transform, "WeaponData");
            weaponInven.SetInfo(weapon.Key.ToString(), weapon.Value);
        }

        GetImage((int)Images.BackgroundImg).gameObject.AddUIEvent(Close);
        
    }

    void Close(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.UI.ClosePopupUI(PopupID);
    }
}
