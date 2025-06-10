using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class TitleScreenManager : MonoBehaviour
{

    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject warningScreen;


    public void StartButton()
    {
        DataPersistenceManager.instance.LoadGame();
        DataPersistenceManager.instance.SaveGame();
        loadingScreen.LoadScene("Lobby");
    }

    public void ResetButton()
    {
        warningScreen.SetActive(true);

    }

    public void YesButton()
    {
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();

        loadingScreen.LoadScene("OpeningCutscene");
    }

    public void NoButton()
    {
        warningScreen.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
