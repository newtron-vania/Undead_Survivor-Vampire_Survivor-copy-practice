using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_TimeStop : UI_Popup
{
    enum Images
    {
        Image
    }

    public override Define.PopupUIGroup PopupID { get { return Define.PopupUIGroup.UI_TimeStop; } }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Image image = GetImage((int)Images.Image);

        StartCoroutine(ChangeImageColor(image, new Color(0.19811f, 0.1188f, 0.082983f, 0.3f)));
    }

    
    IEnumerator ChangeImageColor(Image image, Color color)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        image.color = color;
    }
}
