using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelUp : UI_Popup
{
    int MaxUpgradeNum = 3;
    
    public enum Panels
    {
        GridPanel
    }

    public override Define.PopupUIGroup _popupID
    {
        get { return Define.PopupUIGroup.UI_LevelUp; }
    }
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(Panels));

        GameObject gridPanel = Get<GameObject>((int)Panels.GridPanel);

        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }


        //here we choose stat or weapon random number.
        string title = "패널 테스트";
        string desc = "패널 설명 테스트";
        for(int i = 0; i< MaxUpgradeNum; i++)
        {
            GameObject upgradePanel = Managers.UI.MakeSubItem<UpgdPanel>(parent:gridPanel.transform).gameObject;
            UpgdPanel upgradeDesc = upgradePanel.GetOrAddComponent<UpgdPanel>();
            upgradeDesc.SetInfo(title+i.ToString(),desc+i.ToString());
        }
    }
}
