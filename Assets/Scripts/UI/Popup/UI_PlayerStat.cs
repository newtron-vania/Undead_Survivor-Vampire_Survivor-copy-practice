using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerStat : UI_Popup
{
    PlayerStat player;

    public override Define.PopupUIGroup _popupID { get { return Define.PopupUIGroup.UI_GameMenu; } }


    enum Images
    {
        BackgroundImg,
        StatPanel,
        WeaponPanel
    }

    enum Texts
    {
        MaxHpText,
        DamageText,
        DefenseText,
        MoveSpeedText,
        CooldonwText,
        AmountText,

        KnifeText,
        FirballText,
        SpinText,
        PosionText,
        LightningText,
        ShotgunText
    }
    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));


        
    }
}
