using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameMenu : UI_Popup
{
    enum Buttons
    {
        PlayerStatButton,
        BackToGameButton,
        BackToMainButton
    }

    enum Images
    {
        BackgroundImage,
    }

    enum Texts
    {
        MenuText,
        SoundText,
        SoundSelectText

    }

    enum Sliders
    {
        VolumeSlider
    }

    enum Dropdowns
    {
        BGMSelectorDD
    }

    enum BGMs
    {
        BGM_01,
        BGM_02,
        BGM_03,
        BGM_04
    }

    public override Define.PopupUIGroup _popupID
    {
        get { return Define.PopupUIGroup.UI_GameMenu; }
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Dropdown>(typeof(Dropdowns));

        GetImage((int)Images.BackgroundImage).gameObject.AddUIEvent(OnBackToGame);
        GetButton((int)Buttons.BackToGameButton).gameObject.AddUIEvent(OnBackToGame);
        GetButton((int)Buttons.BackToMainButton).gameObject.AddUIEvent(OnBackToMain);
        GetButton((int)Buttons.PlayerStatButton).gameObject.AddUIEvent(OnShowPlayerStatUI);


    }


    public void OnBackToGame(PointerEventData data)
    {
        Managers.UI.CloseAllGroupPopupUI(Define.PopupUIGroup.UI_GameMenu);
    }
    public void OnBackToMain(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.SceneType.MainMenuScene);
        Managers.GamePlay();
    }

    void OnShowPlayerStatUI(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_PlayerStat>();
    }

    public void OnVolumnChanged()
    {
        Slider volumnSlider = Get<Slider>((int)Sliders.VolumeSlider);
        Managers.Sound.SetAudioVolumn(Define.Sound.Bgm, volumnSlider.value);
        Managers.Sound.SetAudioVolumn(Define.Sound.Effect, volumnSlider.value);
    }

    public void OnValueChanged()
    {
        Dropdown BGMdropdown =  Get<Dropdown>((int)Dropdowns.BGMSelectorDD);
        Managers.Sound.Play(((BGMs)BGMdropdown.value).ToString(), Define.Sound.Bgm);
    }
}
