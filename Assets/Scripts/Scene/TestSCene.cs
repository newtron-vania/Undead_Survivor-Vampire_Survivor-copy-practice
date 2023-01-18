using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSCene : BaseScene
{

    public override Define.SceneType _sceneType { get { return Define.SceneType.GameScene; } }

    public override void Clear()
    {
        
    }

    protected override void Init()
    {
        base.Init();
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player/PlayerTest");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        GameObject Boss = Managers.Resource.Instantiate("Monster/Boss");
    }

}
