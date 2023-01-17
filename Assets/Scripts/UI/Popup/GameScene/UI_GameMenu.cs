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


    Slider volumnSlider;
    TMP_Dropdown BGMdropdown;

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
        Bind<TMP_Dropdown>(typeof(Dropdowns));

        GetImage((int)Images.BackgroundImage).gameObject.AddUIEvent(OnBackToGame);
        GetButton((int)Buttons.BackToGameButton).gameObject.AddUIEvent(OnBackToGame);
        GetButton((int)Buttons.BackToMainButton).gameObject.AddUIEvent(OnBackToMain);
        GetButton((int)Buttons.PlayerStatButton).gameObject.AddUIEvent(OnShowPlayerStatUI);

        volumnSlider = Get<Slider>((int)Sliders.VolumeSlider);
        BGMdropdown = Get<TMP_Dropdown>((int)Dropdowns.BGMSelectorDD);

        volumnSlider.value = Managers.Sound.SoundVolumn;
        BGMdropdown.value = (int)Managers.Sound.BGM;
    }


    public void OnBackToGame(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.UI.CloseAllGroupPopupUI(Define.PopupUIGroup.UI_GameMenu);
    }
    public void OnBackToMain(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.SceneType.MainMenuScene);
        Managers.GamePlay();
    }

    void OnShowPlayerStatUI(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.UI.ShowPopupUI<UI_PlayerStat>();
    }

    public void OnVolumnChanged()
    {
        Managers.Sound.SoundVolumn = volumnSlider.value;
        Managers.Sound.SetAudioVolumn(Define.Sound.Bgm, volumnSlider.value);
        Managers.Sound.SetAudioVolumn(Define.Sound.Effect, volumnSlider.value);
    }

    public void OnValueChanged()
    {
        
        Managers.Sound.Play(((Define.BGMs)BGMdropdown.value).ToString(), Define.Sound.Bgm);
    }
}
