using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject warningScreen;


    void Start()
    {
        StartCoroutine(StreamVideo());

    }

    private IEnumerator StreamVideo()
    {
        Handheld.PlayFullScreenMovie("babaylan_openingcutscene.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        yield return new WaitForEndOfFrame();
    }

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

        loadingScreen.LoadScene("TitleScreen");
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
