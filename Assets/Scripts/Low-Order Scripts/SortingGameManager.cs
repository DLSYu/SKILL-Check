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

    [Header("Dragon Parts")]
    public GameObject splitHead;      // Reference to Split-Head GameObject
    public GameObject splitTail;      // Reference to Split-Tail GameObject
    public GameObject fullDragon;     // Completed dragon

    [Header("Slots")]
    [SerializeField] public List<RelicCheckedSlot> slots = new List<RelicCheckedSlot>();

    [Header("Relics")]
    [SerializeField] public List<GameObject> allRelics;

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

        // Toggle full dragon visibility
        fullDragon.SetActive(allCorrect);

        if (allCorrect)
        {
            // Hide Split-Head, Split-Tail, and all Slots
            if (splitHead != null) splitHead.SetActive(false);
            if (splitTail != null) splitTail.SetActive(false);
            foreach (var slot in slots) slot.gameObject.SetActive(false);

            // Hide all relics
            foreach (var relic in allRelics) relic.SetActive(false);

            if (!isGameCompleted)
            {
                isGameCompleted = true;
                CalculateStars();
                StartCoroutine(LoadEndSceneAfterDelay(1f));
            }
        }
    }

    public void QuickSortCheckCompletion()
    {
        bool allCorrect = slots.All(s => s.IsCorrect);

        if (allCorrect && !isGameCompleted)
        {
            isGameCompleted = true;
            CalculateStars();
            StartCoroutine(LoadEndSceneAfterDelay(1f));

            PlayerPrefs.SetFloat("ElapsedTime", timer);
            PlayerPrefs.Save();

            SceneManager.LoadScene("Sequence_End");

            Destroy(QuickSortSortingGameManager.Instance.gameObject);
            Destroy(this.gameObject);
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