using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponListImage : UI_Base
{
    string imgName;

    enum Images
    {
        WeaponInven,
        WeaponImg,
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
    }


    public void SetInfo(string spriteName)
    {
        Debug.Log($"sprite name : {spriteName}");
        Debug.Log($"{Managers.Resource.Load<Sprite>($"Prefabs/SpriteIcon/{spriteName}")}");
        GetImage((int)Images.WeaponImg).sprite = Managers.Resource.Load<Sprite>($"Prefabs/SpriteIcon/{spriteName}");
    }
}
