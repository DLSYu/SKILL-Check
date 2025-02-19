using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        // Retrieve the saved time and display it
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);
        timerText.text = FormatTime(elapsedTime);
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return $"{minutes:00}:{seconds:00}";
    }
}
