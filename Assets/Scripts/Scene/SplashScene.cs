using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    float _time = 0;
    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        if(_time > 1.5)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
