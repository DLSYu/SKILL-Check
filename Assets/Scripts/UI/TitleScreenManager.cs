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

    private List<DateTime[]> allowedTimes = new List<DateTime[]>();

    private string[] weekPasswords = new string[]
    {
        "test1",
        "test2",
        "test3",
        "test4",
        "test5"

    };

    [SerializeField] private GameObject manualUnlockScreen;
    [SerializeField] private TMP_InputField inputtedText;
    [SerializeField] private TextMeshProUGUI errorText;

    public bool bypassPassword;

    void Start()
    {
        DateTime[] week1 = new DateTime[]
        {
            new DateTime(2025, 2, 21, 11, 0, 0),
            new DateTime(2025, 2, 28, 11, 15, 0)
        };

        DateTime[] week2 = new DateTime[]
        {
            new DateTime(2025, 3, 3, 11, 0, 0),
            new DateTime(2025, 3, 20, 11, 15, 0)
        };
        DateTime[] week3 = new DateTime[]
        {
                new DateTime(2025, 4, 1, 11, 0, 0),
                new DateTime(2025, 4, 7, 11, 15, 0)
        };

        DateTime[] week4 = new DateTime[]
        {
                new DateTime(2025, 5, 1, 11, 0, 0),
                new DateTime(2025, 5, 8, 11, 15, 0)
        };

        DateTime[] week5 = new DateTime[]
        {
                new DateTime(2025, 6, 10, 11, 0, 0),
                new DateTime(2025, 6, 22, 11, 15, 0)
        };

        allowedTimes.Add(week1);
        allowedTimes.Add(week2);
        allowedTimes.Add(week3);
        allowedTimes.Add(week4);
        allowedTimes.Add(week5);

    }

    public void OnSubmit()
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();

        string input = inputtedText.text.Trim((char)8203);


        if (input == IdentifyCurrentWeekPassword(now))
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

    private string IdentifyCurrentWeekPassword(DateTime now)
    {
        string currentPassword = weekPasswords[0];
        for (int i = 0; i < allowedTimes.Count; i++)
        {
            if (now > allowedTimes[i][0])
                currentPassword = weekPasswords[i];
            else
            {
                break;
            }
        }
        Debug.Log("password: " + currentPassword);
        return currentPassword;
    }

    private bool isWithinAllowedTimes(DateTime now)
    {
        bool foundAllowedTime = false;
        for (int i = 0; i < allowedTimes.Count; i++)
        {
            if (now > allowedTimes[i][0] && now < allowedTimes[i][1])
            {
                foundAllowedTime = true;
                break;
            }


        }
        return foundAllowedTime;
    }


    public void StartButton()
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();

        // is not within allowed times
        if (!bypassPassword && !isWithinAllowedTimes(now))
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
