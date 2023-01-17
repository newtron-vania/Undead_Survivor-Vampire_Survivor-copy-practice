using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatInven : UI_Base
{


    enum Images
    {
        StatInven,
        StatImg
    }

    enum Texts
    {
        TitleText,
        LevelText
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public void SetInfo(string statName, int statNum)
    {
        GetImage((int)Images.StatImg).sprite = Managers.Resource.LoadSprite(statName);
        GetText((int)Texts.TitleText).text = statName;
        GetText((int)Texts.LevelText).text = statNum.ToString();
    }

}
