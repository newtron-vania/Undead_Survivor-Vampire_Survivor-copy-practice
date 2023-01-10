using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_ItemBoxOpen : UI_Popup
{
    [SerializeField]
    List<Transform> weaponUILocation;
    List<Define.Weapons> weaponList;

    public override Define.PopupUIGroup _popupID { get { return Define.PopupUIGroup.UI_ItemBoxOpen; } }

    enum Buttons
    {
        ItemBoxButton
    }
    enum Texts
    {
        ItemBoxText
    }
    enum Images
    {
        ItemBoxPanel,
        ItemBoxImage,
        BackgroundImg
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetImage((int)Images.BackgroundImg).gameObject.AddUIEvent(OnOpenChest);
    }

    void OnOpenChest(PointerEventData data)
    {
        weaponList = Managers.Event.SetRandomWeaponfromItemBox();

        for (int i = 0; i < weaponList.Count; i++)
        {
            GameObject go = Managers.Resource.Instantiate("UI/SubItem/WeaponInven", parent: weaponUILocation[i].transform);
            Util.FindChild<Image>(go, "WeaponImg", true).sprite = Managers.Resource.Load<Sprite>($"Prefabs/SpriteIcon/{weaponList[i].ToString()}");
        }
        Get<Image>((int)Images.ItemBoxImage).GetComponent<Animator>().Play("Open");

        Button btn = GetButton((int)Buttons.ItemBoxButton);
        btn.gameObject.SetActive(true);
        btn.gameObject.AddUIEvent(Close);
    }

    void Close(PointerEventData data)
    {
        Managers.Event.SetLevelUpWeaponfromItemBox(weaponList);
        Managers.GamePlay();
        Managers.UI.CloseAllGroupPopupUI(_popupID);
    }
}
