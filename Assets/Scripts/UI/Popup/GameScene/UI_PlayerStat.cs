using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_PlayerStat : UI_Popup
{
    PlayerStat player;

    public override Define.PopupUIGroup _popupID { get { return Define.PopupUIGroup.UI_GameMenu; } }

    enum Objects
    {

        StatPanel,
        WeaponPanel
    }
    enum Images
    {
        BackgroundImg,
    }
    public override void Init()
    {
        player = Managers.Game.getPlayer().GetOrAddComponent<PlayerStat>();
        Bind<GameObject>(typeof(Objects));
        Bind<Image>(typeof(Images));

        GameObject statPanel = GetObject((int)Objects.StatPanel);
        foreach(Transform child in statPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        foreach(KeyValuePair<string, int> stat in player.getPlayerStatData())
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/StatData", statPanel.transform);
            go.GetOrAddComponent<StatInven>().SetInfo(stat.Key, stat.Value);
        }

        GameObject weaponPanel = GetObject((int)Objects.WeaponPanel);
        foreach (Transform child in weaponPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }
        foreach (KeyValuePair<Define.Weapons, int> weapon in player.GetWeaponDict())
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/WeaponData", weaponPanel.transform);
            go.GetOrAddComponent<WeaponInven>().SetInfo(weapon.Key.ToString(), weapon.Value);
        }

        GetImage((int)Images.BackgroundImg).gameObject.AddUIEvent(Close);
        
    }

    void Close(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.UI.ClosePopupUI(_popupID);
    }
}
