using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{


    [SerializeField]
    Text _text;

    enum Buttons
    {
        PointButton,
        BackToGameButton,
        BackToMainButton,
    }
    enum Texts
    {
        GameTime,
        MenuText,
    }

    enum GameObjects
    {
        TestObject,
    }
    
    enum Images
    {
        WeaponListImage,
        CursorCoolTimeImg
    }

    enum slide
    {
        VolumeSlider,
    }

    enum Dropdown
    {
        Dropdown,
    }



    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        
        //UI_Base.BindUIEvent(go, (PointerEventData data) => go.transform.position = data.position, Define.UIEvent.Drag);
    }
}
