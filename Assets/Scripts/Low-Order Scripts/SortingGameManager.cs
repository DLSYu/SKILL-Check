using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class SortingGameManager : MonoBehaviour
{
    public static SortingGameManager Instance;

    [SerializeField] private bool forceCompletion = false;

    private float timer = 0f;
    private bool isGameCompleted = false; // Flag to track completion for timer

    void Awake()
    {
        // Singleton pattern fix
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Dragon stuff
    public GameObject fullDragon;
    public List<RelicCheckedSlot> slots = new List<RelicCheckedSlot>();
    public List<GameObject> allRelics;

    private void Update()
    {
        if (!isGameCompleted) 
        {
            timer += Time.deltaTime;
        }
    }

    public void CheckCompletion()
    {
        bool allCorrect = slots.All(s => s.IsCorrect);

        // Toggle final dragon
        fullDragon.SetActive(allCorrect);

        // Hide ALL dragon parts when completed
        if (allCorrect)
        {
            foreach (var slot in slots)
            {
                if (slot.originalPart != null) slot.originalPart.SetActive(false);
                if (slot.hiddenPart != null) slot.hiddenPart.SetActive(false);
            }

            // Hide relics
            foreach (var relic in allRelics) relic.SetActive(false);
        }

        if (allCorrect && !isGameCompleted)
        {
            isGameCompleted = true;
            CalculateStars();
            StartCoroutine(LoadEndSceneAfterDelay(1f));
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

        // Show dragon for 2 seconds before transition
        if (fullDragon != null)
        {
            fullDragon.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }

        PlayerPrefs.SetFloat("ElapsedTime", timer);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Sequence_End");
    }
}