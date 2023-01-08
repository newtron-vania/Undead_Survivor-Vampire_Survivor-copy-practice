using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : UI_Scene
{
    enum Texts
    {
        GameTime
    }

    enum Images
    {
        WeaponListImage,
        CursorCoolTimeImg
    }

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
    }

    public void SetGameTime()
    {
        Get<TextMeshProUGUI>((int)Texts.GameTime, typeof(Texts)).text = string.Format("{0:D2}:{1:D2}", (int)Mathf.Floor(Managers.GameTime / 60),
            (int)Mathf.Floor(Managers.GameTime % 60));
    }

    public void ActiveCheckCursorImage()
    {
        GameObject cursorCoolTimeImgGo = Get<Image>((int)Images.CursorCoolTimeImg, typeof(Images)).gameObject;

        cursorCoolTimeImgGo.SetActive(true);
    }
}
