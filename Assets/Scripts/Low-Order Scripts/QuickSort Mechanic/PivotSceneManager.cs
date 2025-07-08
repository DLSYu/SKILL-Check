using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PivotSceneManager : MonoBehaviour
{
    private static List<GameObject> shuffledRelicParts;
    private static GameObject pivot;
    private static int currRelicPartIndex = 0;
    private static int relicsChecked = 0;

    private static bool rightOrder = true;

    [SerializeField] private GameObject pivotSlot;
    [SerializeField] private GameObject relicPartSlot;

    private GameObject relicBeingSorted;

    [SerializeField] private GameObject failureTextbox;

    // Start is called before the first frame update
    void Start()
    {
        //set-up for QuickSortSortingGameManager
        List<GameObject> initRelicParts = new List<GameObject>();

        for (int i = 0; i < QuickSortSortingGameManager.Instance.transform.GetChild(0).childCount; i++)
        {
            initRelicParts.Add(QuickSortSortingGameManager.Instance.transform.GetChild(0).GetChild(i).gameObject);
        }

        QuickSortSortingGameManager.Instance.SetRelicPartsList(initRelicParts);

        //set up for PivotSceneManager
        shuffledRelicParts = QuickSortSortingGameManager.Instance.shuffledRelicParts;
        pivot = QuickSortSortingGameManager.Instance.GetPivot();

        if (pivot.GetComponent<StorySegment>().order ==
            shuffledRelicParts[currRelicPartIndex].GetComponent<StorySegment>().order)
        {
            currRelicPartIndex++;
        }

        relicBeingSorted = QuickSortSortingGameManager.Instance.PutRelicPart(relicPartSlot, shuffledRelicParts[currRelicPartIndex]);
        relicsChecked++;

        relicBeingSorted.GetComponent<RelicMovement>().isPivotScene = true;
        relicBeingSorted.GetComponent<RelicMovement>().useWorldSpace = false;
        relicBeingSorted.GetComponent<RelicMovement>().isMovable = true;
        relicBeingSorted.GetComponent<RelicMovement>().pivotSceneManager = gameObject;

        QuickSortSortingGameManager.Instance.PutRelicPart(pivotSlot, pivot);

        Debug.Log($"shuffled index: {currRelicPartIndex}");
        Debug.Log($"shuffledRelicParts[currRelicPartIndex]: {shuffledRelicParts[currRelicPartIndex].name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfterButtonClick()
    {
        bool result = QuickSortSortingGameManager.Instance.IsQuickSortCorrect(shuffledRelicParts[currRelicPartIndex], true);
        rightOrder = result && rightOrder;

        ResetScene();
    }

    public void BeforeButtonClick()
    {
        bool result = QuickSortSortingGameManager.Instance.IsQuickSortCorrect(shuffledRelicParts[currRelicPartIndex], false);
        rightOrder = result && rightOrder;

        ResetScene();
    }

    private void ResetScene()
    {
        relicBeingSorted.GetComponent<RelicMovement>().isPivotScene = false;
        relicBeingSorted.GetComponent<RelicMovement>().isMovable = false;
        relicBeingSorted.GetComponent<RelicMovement>().useWorldSpace = true;

        if (relicsChecked >= shuffledRelicParts.Count - 1)
        {
            if (rightOrder)
            {
                Debug.Log($"shuffled index: {currRelicPartIndex}");
                SortingGameManager.Instance.transform.SetParent(QuickSortSortingGameManager.Instance.transform);
                SceneManager.LoadScene("QuickSort_SortingScene");
            }
            else
            {
                Debug.Log("WRONG ORDER. TRY AGAIN.");

                failureTextbox.SetActive(true);

                currRelicPartIndex = 0;
                relicsChecked = 0;
                rightOrder = true;

                if (pivot.GetComponent<StorySegment>().order ==
                shuffledRelicParts[currRelicPartIndex].GetComponent<StorySegment>().order)
                {
                    currRelicPartIndex++;
                }

                relicBeingSorted = QuickSortSortingGameManager.Instance.PutRelicPart(relicPartSlot, shuffledRelicParts[currRelicPartIndex]);
                relicsChecked++;

                relicBeingSorted.GetComponent<RelicMovement>().isPivotScene = true;
                relicBeingSorted.GetComponent<RelicMovement>().isMovable = true;
                relicBeingSorted.GetComponent<RelicMovement>().useWorldSpace = false;
                relicBeingSorted.GetComponent<RelicMovement>().pivotSceneManager = gameObject;

                Debug.Log($"shuffled index: {currRelicPartIndex}");
                Debug.Log($"shuffledRelicParts[currRelicPartIndex]: {shuffledRelicParts[currRelicPartIndex].name}");
            }
        }
        else
        {
            currRelicPartIndex++;

            // if next relic is same as pivot relic, skip this relic
            if (pivot.GetComponent<StorySegment>().order ==
                shuffledRelicParts[currRelicPartIndex].GetComponent<StorySegment>().order)
            {
                //if (currRelicPartIndex >= shuffledRelicParts.Count - 1)
                //{
                //    currRelicPartIndex = 0;
                //}
                //else
                //{
                //    currRelicPartIndex++;
                //}

                currRelicPartIndex++;

            }

            relicBeingSorted = QuickSortSortingGameManager.Instance.PutRelicPart(relicPartSlot, shuffledRelicParts[currRelicPartIndex]);
            relicsChecked++;

            relicBeingSorted.GetComponent<RelicMovement>().isPivotScene = true;
            relicBeingSorted.GetComponent<RelicMovement>().isMovable = true;
            relicBeingSorted.GetComponent<RelicMovement>().useWorldSpace = false;
            relicBeingSorted.GetComponent<RelicMovement>().pivotSceneManager = gameObject;

            Debug.Log($"shuffled index: {currRelicPartIndex}");
        }
    }
}
