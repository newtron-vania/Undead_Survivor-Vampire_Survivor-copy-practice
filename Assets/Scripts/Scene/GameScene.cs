using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameScene : MonoBehaviour
{
    private UI_Player playerUI;
    private void Awake()
    {
        Init();
    }

    void Init()
    {
        playerUI = Managers.UI.ShowSceneUI<UI_Player>("UI_Player");
    }
    private void Update()
    {
        playerUI.SetGameTime();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!Managers.gameStop)
            {
                Managers.UI.ShowPopupUI<UI_GameMenu>("UI_GameMenu");
                Managers.GamePause();
                Debug.Log($"Game Pause! - gameStop : {Managers.gameStop}");
            }
            else
            {
                Managers.UI.ClosePopupUI(Define.PopupUIGroup.UI_GameMenu);
                Managers.GamePlay();
                Debug.Log($"Game Play! - gameStop : {Managers.gameStop}");
            }
                
        }
    }
}
