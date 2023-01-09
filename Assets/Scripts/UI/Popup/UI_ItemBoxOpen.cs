using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]
public class WeaponSprite : SerializableDictionary<Define.Weapons, Sprite> { }
public class UI_ItemBoxOpen : UI_Popup
{
    [SerializeField]
    WeaponSprite _weaponUIImage;
    [SerializeField]
    List<GameObject> weaponUILocation;  

    public override Define.PopupUIGroup _popupID { get { return Define.PopupUIGroup.UI_ItemBoxOpen; } }

    enum Texts
    {
        ItemBoxText
    }
    enum Images
    {
        ItemBoxPanel,
        ItemBoxImage
    }

    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        //Todo
        //random count and Event.ChooseRandomWeapon
        List<Define.Weapons> weaponList = Managers.Event.SetRandomWeaponfromItemBox();

    }

    void Update()
    {
        
    }
}
