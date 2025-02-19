using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SortingGameManager : MonoBehaviour
{
    public static SortingGameManager Instance;
    public RelicCheckedSlot[] relicPlaces; // Array of RelicPlace objects

    private float timer = 0f;
    private bool isGameCompleted = false; // Flag to track completion for timer

    void Awake() => Instance = this;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void CheckCompletion()
    {
        foreach (var place in relicPlaces)
        {
            if (!place.IsCorrect)
            {
                Debug.Log($"Incorrect relic in place: {place.name}");
                return;
            }
        }

        isGameCompleted = true; // Stop the timer

        // All relics are correct
        StartCoroutine(LoadEndSceneAfterDelay(2f)); // 2-second delay
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