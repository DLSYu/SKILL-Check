using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class SortingGameManager : MonoBehaviour
{
    public static SortingGameManager Instance;

    public Image fullDragonImage;

    [SerializeField] private bool forceCompletion = false;

    public List<RelicCheckedSlot> slots = new List<RelicCheckedSlot>();

    private float timer = 0f;
    private bool isGameCompleted = false; // Flag to track completion for timer

    void Awake() => Instance = this;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void CheckCompletion()
    {
        bool allCorrect = true;

        // Debug print slot states
        foreach (var slot in slots)
        {
            Debug.Log($"{slot.name} correct: {slot.IsCorrect}");
            if (!slot.IsCorrect) allCorrect = false;
        }

        // Force completion for testing
        if (forceCompletion) allCorrect = true;

        // Use SetActive() instead of enabled for GameObject
        if (fullDragonImage != null)
        {
            fullDragonImage.gameObject.SetActive(allCorrect);
            Debug.Log($"Full dragon active: {allCorrect}");
        }

        if (allCorrect && !isGameCompleted)
        {
            isGameCompleted = true;
            Debug.Log("ALL CORRECT! Starting transition...");
            StartCoroutine(LoadEndSceneAfterDelay(3f));
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
        // Show dragon for 2 seconds before transition
        if (fullDragonImage != null)
        {
            fullDragonImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Sequence_End");
    }
}