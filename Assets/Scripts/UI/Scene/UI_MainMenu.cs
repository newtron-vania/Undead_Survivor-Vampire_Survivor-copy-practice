using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{

    void OnBackToGame()
    {
        gameObject.SetActive(false);
        Managers.GamePlay();
    }
    void OnBackToMain()
    {
        Managers.GamePlay();
    }
}
