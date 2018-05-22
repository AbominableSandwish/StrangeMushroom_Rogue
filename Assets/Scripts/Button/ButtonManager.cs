using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    MAIN_MENU = 0,
    TUTORIAL = 1,
    STRANGE = 2,
    GAME_OVER = 3
}

public class ButtonManager : MonoBehaviour
{

    public void SceneLoad_Tutorial()
    {
        SceneManager.LoadScene((int)Scene.TUTORIAL);
    }

    public void SceneLoad_Exit()
    {
        Application.Quit();
    }

    public void SceneLoad_MainMenu()
    {
        SceneManager.LoadScene((int)Scene.MAIN_MENU);
    }
}
