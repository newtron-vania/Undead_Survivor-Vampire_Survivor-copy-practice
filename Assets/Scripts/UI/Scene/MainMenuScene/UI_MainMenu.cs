using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_MainMenu : UI_Scene
{
    enum Images
    {
        BackgroundImg,
        FrontImage,
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

    private Animator _anime;
    private Image backgroundImg;
    private bool _animeOver;


    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(ImageObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        backgroundImg = GetImage((int)Images.BackgroundImg);
        _anime = backgroundImg.transform.GetComponent<Animator>();

        GetImage((int)Images.FrontImage).gameObject.AddUIEvent(SetAnimationOver);

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
            Managers.Resource.Destroy(GetImage((int)Images.FrontImage).gameObject);
        }
    }
    void ShowCharacterSelectUI(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Debug.Log("Show!");
        Managers.UI.ShowPopupUI<UI_CharacterSelect>();
    }

    void ExitGame(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
