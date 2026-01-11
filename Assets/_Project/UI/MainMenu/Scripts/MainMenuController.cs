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

    public void NewGame()
    {
        if(SaveSystem.Instance.Data == null)
            SaveSystem.Instance.CreateNewSave();
        else
        {
            SaveSystem.Instance.Load();
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("ChoixHero");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
