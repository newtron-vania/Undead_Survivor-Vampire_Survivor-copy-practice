using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{

    enum GameObjects
    {
        GridPanel
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for(int i=0; i<8; i++)
        {
            GameObject Item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent : gridPanel.transform).gameObject;


            //Item.getAddComponent<>
            UI_Inven_Item invenItem = Item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집행검{i + 1}번");
        }

    }
}
