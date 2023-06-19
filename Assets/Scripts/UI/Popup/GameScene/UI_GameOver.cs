using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_GameOver : UI_Popup
{
    enum Images
    {
        PretectImg,
        GameOverImg,
        DeadImg
    }

    enum Buttons
    {
        BackToMainButton
    }


    public override Define.PopupUIGroup PopupID { get { return Define.PopupUIGroup.UI_GameOver; } }
    public RuntimeAnimatorController[] _animeCon;
    private Animator _anime;


    public override void Init()
    {
        base.Init();
        Managers.Sound.Play("Dead");
        _anime = gameObject.FindChild<Animator>("DeadImg");
        _anime.runtimeAnimatorController = _animeCon[Managers.Game.StartPlayer.id - 1];
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetImage((int)Images.PretectImg).gameObject.AddUIEvent(OnClickFinishAnime);
        GetButton((int)Buttons.BackToMainButton).gameObject.AddUIEvent(OnClickBackToMain);

    }

    void OnClickFinishAnime(PointerEventData data)
    {
        _anime.Play("GameOverAnime", -1, 1f);
        GetButton((int)Buttons.BackToMainButton).gameObject.SetActive(true);
    }

    void OnClickBackToMain(PointerEventData data)
    {
        Managers.Sound.Play("Select", Define.Sound.Effect);
        Managers.Scene.LoadScene(Define.SceneType.MainMenuScene);
        Managers.GamePlay();
    }
}
