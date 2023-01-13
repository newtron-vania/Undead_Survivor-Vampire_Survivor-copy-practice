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
        CooldownText,
        AmountText,

        KnifeText,
        FireballText,
        SpinText,
        PoisonText,
        LightningText,
        ShotgunText
    }

    public override void Init()
    {
        player = Managers.Game.getPlayer().GetOrAddComponent<PlayerStat>();
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        foreach(Texts text in System.Enum.GetValues(typeof(Texts)))
        {
            int level = 0;
            switch (text)
            {
                case Texts.MaxHpText:
                    GetText((int)text).text = player.MaxHP.ToString();
                    break;
                case Texts.DamageText:
                    GetText((int)text).text = player.Damage.ToString();
                    break;
                case Texts.DefenseText:
                    GetText((int)text).text = player.Defense.ToString();
                    break;
                case Texts.MoveSpeedText:
                    GetText((int)text).text = player.MoveSpeed.ToString();
                    break;
                case Texts.CooldownText:
                    GetText((int)text).text = player.Cooldown.ToString();
                    break;
                case Texts.AmountText:
                    GetText((int)text).text = player.Amount.ToString();
                    break;
                case Texts.KnifeText:
                    player.GetWeaponDict().TryGetValue(Define.Weapons.Knife, out level);
                    GetText((int)text).text = level.ToString();
                    break;
                case Texts.FireballText:
                    player.GetWeaponDict().TryGetValue(Define.Weapons.Fireball, out level);
                    GetText((int)text).text = level.ToString();
                    break;
                case Texts.SpinText:
                    player.GetWeaponDict().TryGetValue(Define.Weapons.Spin, out level);
                    GetText((int)text).text = level.ToString();
                    break;
                case Texts.PoisonText:
                    player.GetWeaponDict().TryGetValue(Define.Weapons.Poison, out level);
                    GetText((int)text).text = level.ToString();
                    break;
                case Texts.LightningText:
                    player.GetWeaponDict().TryGetValue(Define.Weapons.Lightning, out level);
                    GetText((int)text).text = level.ToString();
                    break;
                case Texts.ShotgunText:
                    player.GetWeaponDict().TryGetValue(Define.Weapons.Shotgun, out level);
                    GetText((int)text).text = level.ToString();
                    break;
            }
        }

        GetImage((int)Images.BackgroundImg).gameObject.AddUIEvent(Close);
        
    }

    void Close(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(_popupID);
    }
}
