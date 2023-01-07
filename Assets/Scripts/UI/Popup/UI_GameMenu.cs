using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameMenu : UI_Popup
{
    enum Buttons
    {
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

    public override void Init()
    {
        _popupID = Define.PopupUIGroup.GameMenuUI;
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Dropdown>(typeof(Dropdowns));

        GetImage((int)Images.BackgroundImage).gameObject.AddUIEvent(OnBackToGame);
        GetButton((int)Buttons.BackToGameButton).gameObject.AddUIEvent(OnBackToGame);
        GetButton((int)Buttons.BackToMainButton).gameObject.AddUIEvent(OnBackToMain);
    }


    public void OnBackToGame(PointerEventData data)
    {
        gameObject.SetActive(false);
        Managers.GamePlay();
    }
    public void OnBackToMain(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.SceneType.MainMenuScene);
        Managers.GamePlay();
    }
}
