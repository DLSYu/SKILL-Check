using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class TitleScreenManager : MonoBehaviour
{

    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject warningScreen;

    [SerializeField] private GameObject manualUnlockScreen;
    [SerializeField] private TMP_InputField inputtedText;
    [SerializeField] private TextMeshProUGUI errorText;
    public bool bypassPassword;


    public void OnSubmit()
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();

        string input = inputtedText.text.Trim((char)8203);


        if (input == TimeCheckInstance.instance.IdentifyCurrentWeekPassword())
        {
            manualUnlockScreen.SetActive(false);
            DataPersistenceManager.instance.LoadGame();
            DataPersistenceManager.instance.SaveGame();
            loadingScreen.LoadScene("Lobby");

        }
        else
        {
            errorText.text = "Mali ang password.";
        }
    }



    public void StartButton()
    {

        // is not within allowed times
        if (!bypassPassword && !TimeCheckInstance.instance.isWithinAllowedTimes())
        {
            manualUnlockScreen.SetActive(true);
        }
        else
        {
            DataPersistenceManager.instance.LoadGame();
            DataPersistenceManager.instance.SaveGame();
            loadingScreen.LoadScene("Lobby");
        }
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
