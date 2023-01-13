using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_MainMenu : UI_Scene
{
    Animator _anime;
    Image backgroundImg;
    bool _animeOver;


    enum Images
    {
        BackgroundImg,
        Logo,
    }

    enum ImageObjects
    {
        Farmers,
        Monsters
    }
    
    enum Texts
    {
        PresstoStartText
    }

    enum Buttons
    {
        GamePlayButton,
        GameExitButton
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(ImageObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        backgroundImg = GetImage((int)Images.BackgroundImg);
        _anime = backgroundImg.transform.GetComponent<Animator>();

        backgroundImg.gameObject.AddUIEvent(SetAnimationOver);


        GetButton((int)Buttons.GamePlayButton).gameObject.AddUIEvent(ShowCharacterSelectUI);
        GetButton((int)Buttons.GameExitButton).gameObject.AddUIEvent(ExitGame);
    }

    void SetAnimationOver(PointerEventData data)
    {
        _animeOver = backgroundImg.transform.GetComponent<AnimeOver>()._animeOver;
        if (!_animeOver)
        {
            _anime.Play("MainGameStartAnime", -1, 1.0f);
        }
        else
        {
            Managers.Resource.Destroy(GetText((int)Texts.PresstoStartText).gameObject);
            foreach(Buttons button in System.Enum.GetValues(typeof(Buttons)))
            {
                GetButton((int)button).gameObject.SetActive(true);
            }
        }
    }
    void ShowCharacterSelectUI(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_CharacterSelect>();
    }

    void ExitGame(PointerEventData data)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
