using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    float time = 0;
    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if(time > 1.5)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
