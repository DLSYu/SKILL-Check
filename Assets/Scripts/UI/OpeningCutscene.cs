using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class OpeningCutscene : MonoBehaviour
{

    [SerializeField] private LoadingScreen loadingScreen;

    void Start()
    {

        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        // Handheld.PlayFullScreenMovie("babaylan_openingcutscene.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        yield return new WaitForEndOfFrame();

        loadingScreen.LoadScene("TitleScreen");


    }

}
