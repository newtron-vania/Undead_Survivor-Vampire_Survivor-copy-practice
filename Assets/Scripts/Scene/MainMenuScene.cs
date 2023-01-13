using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScene : BaseScene
{
    public override Define.SceneType _sceneType { get { return Define.SceneType.MainMenuScene; } }


    protected override void Init()
    {
        base.Init();
        Managers.UI.ShowSceneUI<UI_MainMenu>();
    }
    public override void Clear()
    {
        
    }
}
