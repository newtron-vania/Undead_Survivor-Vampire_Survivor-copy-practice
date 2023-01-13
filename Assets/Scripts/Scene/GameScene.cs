using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

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
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player/Player0");
        player.GetComponent<PlayerStat>().AddOrSetWeaponDict(Define.Weapons.Shotgun, 1, true);
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);
        Managers.Resource.Instantiate("Content/Grid");
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
            }
            else
            {
                Managers.UI.ClosePopupUI(Define.PopupUIGroup.UI_GameMenu);
            }
        }
    }

    void SetActiveSkillCursorImg()
    {
        if (!Managers.Game.getPlayer().GetOrAddComponent<PlayerStat>().GetWeaponDict().TryGetValue(Define.Weapons.Lightning, out int weapon))
            return;
        Image cursorCoolTimeImg = playerUI.gameObject.FindChild<Image>("CursorCoolTimeImg");
        if(Managers.UI.GetPopupUICount() == 0)
            cursorCoolTimeImg.gameObject.SetActive(true);
        else
            cursorCoolTimeImg.gameObject.SetActive(false);
    }
}
