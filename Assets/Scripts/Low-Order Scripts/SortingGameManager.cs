using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SortingGameManager : MonoBehaviour
{
    public static SortingGameManager Instance;

    // Assign the RelicCheckedSlot component on the dragon in the Inspector.
    public RelicCheckedSlot relicDragon;

    private float timer = 0f;
    private bool isGameCompleted = false; // Flag to track completion for timer

    void Awake() => Instance = this;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void CheckCompletion()
    {
        // Check if the relic sequence is complete on the dragon
        if (relicDragon.IsSequenceComplete)
        {
            isGameCompleted = true; // Stop the timer
            CalculateStars(); // Evaluate score based on time
            Debug.Log("Sequence complete! Loading next scene...");
            StartCoroutine(LoadEndSceneAfterDelay(2f)); // 2-second delay
        }
    }

    private void CalculateStars()
    {
        int stars = 1; // Default: 1 star
        if (timer <= 60) stars = 3;    // ≤1 minute: 3 stars
        else if (timer <= 120) stars = 2; // ≤2 minutes: 2 stars

        PlayerPrefs.SetInt("StarCount", stars);
        PlayerPrefs.Save();
    }

    IEnumerator LoadEndSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Save the elapsed time to PlayerPrefs
        PlayerPrefs.SetFloat("ElapsedTime", timer);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Sequence_End");
    }
}