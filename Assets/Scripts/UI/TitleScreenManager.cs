using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }

    public void SettingsButton()
    {
        // Options Menu Slides in
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
