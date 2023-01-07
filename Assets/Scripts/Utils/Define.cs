using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
        Weapon
    }

    public enum PopupUIGroup
    {
        Unknown,
        UI_GameMenu,
        UI_ItemBox,
        UI_LevelUp
    }

    public enum SceneUI
    {
        Unknown,
        UI_Player,
        UI_MainMenu,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum SceneType
    {
        Unknown,
        GameScene,
        MainMenuScene
    }

}
