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

    private static bool rightOrder = true;

    [SerializeField] private GameObject pivotSlot;
    [SerializeField] private GameObject relicPartSlot;

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

        QuickSortSortingGameManager.Instance.PutRelicPart(relicPartSlot, shuffledRelicParts[currRelicPartIndex]);
        QuickSortSortingGameManager.Instance.PutRelicPart(pivotSlot, pivot);

        Debug.Log($"shuffled index: {currRelicPartIndex}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfterButtonClick()
    {
        bool result = QuickSortSortingGameManager.Instance.IsQuickSortCorrect(shuffledRelicParts[currRelicPartIndex], true);
        rightOrder = result && rightOrder;

        currRelicPartIndex++;
        if (pivot.GetComponent<StorySegment>().order == 
            shuffledRelicParts[currRelicPartIndex].GetComponent<StorySegment>().order)
        {
            currRelicPartIndex++;
        }

        ResetScene();
    }

    public void BeforeButtonClick()
    {
        bool result = QuickSortSortingGameManager.Instance.IsQuickSortCorrect(shuffledRelicParts[currRelicPartIndex], false);
        rightOrder = result && rightOrder;

        currRelicPartIndex++;
        if (pivot.GetComponent<StorySegment>().order ==
            shuffledRelicParts[currRelicPartIndex].GetComponent<StorySegment>().order)
        {
            currRelicPartIndex++;
        }

        ResetScene();
    }

    private void ResetScene()
    {
        Debug.Log($"shuffled index: {currRelicPartIndex}");

        if(currRelicPartIndex >= shuffledRelicParts.Count - 1)
        {
            if (rightOrder)
            {
                SceneManager.LoadScene("QuickSort_SortingScene");
            }
            else
            {
                Debug.Log("WRONG ORDER. TRY AGAIN.");

                currRelicPartIndex = 0;
                rightOrder = true;
            }
        }
        else
        {
            QuickSortSortingGameManager.Instance.PutRelicPart(relicPartSlot, shuffledRelicParts[currRelicPartIndex]);

        }
    }
}
