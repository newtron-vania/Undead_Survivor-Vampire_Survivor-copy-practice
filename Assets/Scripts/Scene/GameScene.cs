using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private GameObject playerUI;
    private void Awake()
    {
        playerUI = Managers.UI.ShowFullUI("PlayerUI");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!Managers.gameStop)
            {
                Managers.UI.ShowPopupUI("MenuUI");
                playerUI.transform.Find("CursorCoolTimeImg").gameObject.SetActive(false);
                Debug.Log($"Game Pause! - gameStop : {Managers.gameStop}");
            }
            else
            {
                Managers.UI.CloseCurUI();
                playerUI.transform.Find("CursorCoolTimeImg").gameObject.SetActive(true);
                Debug.Log($"Game Play! - gameStop : {Managers.gameStop}");
            }
                
        }
    }
}
