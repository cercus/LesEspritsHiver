using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour
{

    public void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        //SceneManager.LoadScene(scene);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
