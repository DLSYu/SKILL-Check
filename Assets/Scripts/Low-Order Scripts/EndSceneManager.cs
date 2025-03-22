using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    [Header("Timer Display")]
    [SerializeField] private TMP_Text timerText;

    [Header("Star Animation")]
    [SerializeField] private GameObject[] stars; // Assign Star1, Star2, Star3 in Inspector
    [SerializeField] private float starAnimationDelay = 0.5f; // Delay between star animations

    void Start()
    {
        // Show the timer first
        float elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);
        timerText.text = FormatTime(elapsedTime);

        // Start star animation coroutine
        StartCoroutine(ActivateStars());
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return $"{minutes:00}:{seconds:00}";
    }

    IEnumerator ActivateStars()
    {
        // Get star count from PlayerPrefs
        int starCount = PlayerPrefs.GetInt("StarCount", 1);

        // Deactivate all stars initially
        foreach (var star in stars)
        {
            star.SetActive(false);
        }

        // Add delay before stars appear 
        yield return new WaitForSeconds(0.5f);

        // Wait until stars are properly initialized
        yield return new WaitForEndOfFrame();

        // Activate stars one by one with animation
        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
            stars[i].GetComponent<Animator>().Play("StarAppear");
            yield return new WaitForSeconds(starAnimationDelay);
        }
    }
}
