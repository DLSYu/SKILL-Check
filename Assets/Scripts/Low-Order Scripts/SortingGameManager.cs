using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class SortingGameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string stageID = "";
    public static SortingGameManager Instance;
    [SerializeField] private GameObject uiAnimatorDragon;

    [SerializeField] private bool forceCompletion = false;

    [Header("Dragon Parts")]
    public GameObject splitHead;      // Reference to Split-Head GameObject
    public GameObject splitTail;      // Reference to Split-Tail GameObject

    [Header("Slots")]
    [SerializeField] public List<RelicCheckedSlot> slots = new List<RelicCheckedSlot>();

    [Header("Relics")]
    [SerializeField] public List<GameObject> allRelics;

    private float timer = 0f;
    private bool isGameCompleted = false; // Flag to track completion for timer

    // save file shenanigans
    private LowOrderStageAnalytics lowOrderStageAnalytics;
    private bool hasSaved = false;
    private List<string> relicAnswers;
    void Awake()
    {
        relicAnswers = new List<string>();

        for (int i = 0; i < slots.Count; i++)
        {
            relicAnswers.Add("");
        }
        lowOrderStageAnalytics = new LowOrderStageAnalytics(System.DateTime.Now.ToString(), stageID);
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

        bool hasChanges = false;
        int allRelicsCount = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].placedRelic == null)
            {
                break;
            }
            else
            {
                allRelicsCount++;

                if (slots[i].placedRelic.name != relicAnswers[i])
                {
                    hasChanges = true;
                    relicAnswers[i] = slots[i].placedRelic.name;
                }
            }
        }


        if (allCorrect)
        {
            uiAnimatorDragon.SetActive(true);
            // Hide Split-Head, Split-Tail, and all Slots
            if (splitHead != null) splitHead.SetActive(false);
            if (splitTail != null) splitTail.SetActive(false);
            foreach (var slot in slots) slot.gameObject.SetActive(false);

            // Hide all relics
            foreach (var relic in allRelics) relic.SetActive(false);

            if (!isGameCompleted)
            {
                isGameCompleted = true;
                lowOrderStageAnalytics.FinishGameTime(timer, System.DateTime.Now.ToString());
                CalculateStars();
                StartCoroutine(LoadEndSceneAfterDelay(1f));
            }
        }
        else if (hasChanges && allRelicsCount == slots.Count)
        {
            lowOrderStageAnalytics.IncrementMistakes();
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

    public void LoadData(GameData data)
    {
        // nothing
    }

    public void SaveData(GameData data)
    {
        Debug.Log("got here");
        if (!hasSaved)
        {
            if (isGameCompleted)
            {
                bool value;
                data.stageCompletionDictionary.TryGetValue(stageID, out value);

                if (!value)
                    data.stageCompletionDictionary.Add(stageID, true);
            }

            Debug.Log("got here again");

            data.lowOrderStageAnalyticsList.Add(lowOrderStageAnalytics);
            hasSaved = true;
        }


    }

    IEnumerator LoadEndSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Show dragon for 2 seconds before transition
        if (uiAnimatorDragon != null)
        {
            uiAnimatorDragon.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
        }

        PlayerPrefs.SetFloat("ElapsedTime", timer);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Sequence_End");
    }

    private void OnDestroy()
    {
        DataPersistenceManager.instance.SaveGame();
    }
}