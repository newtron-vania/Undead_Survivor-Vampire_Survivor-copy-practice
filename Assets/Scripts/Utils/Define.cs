using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    enum MonsterType
    {
        Zombie1,
        Zombie2,
    }
    public enum WorldObject
    {
        Unknown,
        Player,
        Enemy,
        Weapon
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
    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum SceneType
    {
        Unknown,
        GameScene,
        MainMenuScene
    }
    public enum CameraMode
    {
        QuarterView,
    }

}
