using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    public void OnBackToGame()
    {
        gameObject.SetActive(false);
        Managers.GamePlay();
    }
    public void OnBackToMain()
    {
        Managers.Scene.LoadScene(Define.SceneType.MainMenuScene);
        Managers.GamePlay();
    }
}
