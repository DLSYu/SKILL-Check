using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    public void StartButton()
    {
        DataPersistenceManager.instance.LoadGame();
        DataPersistenceManager.instance.SaveGame();
        loadingScreen.LoadScene("Lobby");
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
