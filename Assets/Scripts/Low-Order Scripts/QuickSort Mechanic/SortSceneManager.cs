using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SortSceneManager : MonoBehaviour
{
    private List<GameObject> beforeRelics = new List<GameObject>();
    [SerializeField] private GameObject beforeInventorySlots;
    [SerializeField] private GameObject beforeCheckedSlots;
    private List<GameObject> beforeCheckedSlotsList = new List<GameObject>();

    private List<GameObject> afterRelics = new List<GameObject>();
    [SerializeField] private GameObject afterInventorySlots;
    [SerializeField] private GameObject afterCheckedSlots;
    private List<GameObject> afterCheckedSlotsList = new List<GameObject>();

    private GameObject pivotRelic;
    [SerializeField] private GameObject pivotSlot;

    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject checkedSlotPrefab;

    //For Canvas Movements
    [SerializeField] private GameObject canvas;
    [SerializeField] private Vector3 leftCanvasPos;
    [SerializeField] private Vector3 rightCanvasPos;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    private bool isInLeftScreen = true;
    private bool changeCanvasPos = false;
    private float movePercentage = 0f;
    private float elapsedTime = 0f;
    [SerializeField] private float canvasMoveDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < QuickSortSortingGameManager.Instance.transform.GetChild(0).childCount; i++)
        {
            GameObject toSetUp = QuickSortSortingGameManager.Instance.transform.GetChild(0).GetChild(i).gameObject;

            if(toSetUp.GetComponent<StorySegment>().order == QuickSortSortingGameManager.Instance.pivotRandIndex)
            {
                
                Instantiate(toSetUp, pivotSlot.transform);
                toSetUp.transform.localPosition = Vector3.zero;
            }
            else if (toSetUp.GetComponent<StorySegment>().order < QuickSortSortingGameManager.Instance.pivotRandIndex)
            {
                beforeRelics.Add(toSetUp);

                GameObject checkedSlot = Instantiate(checkedSlotPrefab, beforeCheckedSlots.transform);
                //checkedSlot.GetComponent<RelicCheckedSlot>().correctRelic = toSetUp;
                beforeCheckedSlotsList.Add(checkedSlot);
                SortingGameManager.Instance.slots.Add(checkedSlot.GetComponent<RelicCheckedSlot>());

            }
            else if (toSetUp.GetComponent<StorySegment>().order > QuickSortSortingGameManager.Instance.pivotRandIndex)
            {
                afterRelics.Add(toSetUp);

                GameObject checkedSlot = Instantiate(checkedSlotPrefab, afterCheckedSlots.transform);
                //checkedSlot.GetComponent<RelicCheckedSlot>().correctRelic = toSetUp;
                afterCheckedSlotsList.Add(checkedSlot);
                SortingGameManager.Instance.slots.Add(checkedSlot.GetComponent<RelicCheckedSlot>());
            }
        }

        int numeralCount = 1;

        foreach (RelicCheckedSlotNumeralChanger changer in beforeCheckedSlots.transform.GetComponentsInChildren<RelicCheckedSlotNumeralChanger>())
        {
            changer.ChangeNumeral(numeralCount.ToString());
            numeralCount++;
        }

        numeralCount++;

        foreach (RelicCheckedSlotNumeralChanger changer in afterCheckedSlots.transform.GetComponentsInChildren<RelicCheckedSlotNumeralChanger>())
        {
            changer.ChangeNumeral(numeralCount.ToString());
            numeralCount++;
        }

        QuickSortSortingGameManager.Instance.Shuffle<GameObject>(beforeRelics);
        QuickSortSortingGameManager.Instance.Shuffle<GameObject>(afterRelics);

        foreach (GameObject relic in beforeRelics) 
        {
            GameObject inventorySlot = Instantiate(inventorySlotPrefab, beforeInventorySlots.transform);

            relic.GetComponent<RelicMovement>().isMovable = true;
            if (relic.transform.parent.GetComponent<RelicSlot>() != null)
            {
                relic.GetComponent<RelicMovement>().originalParent = transform.parent.GetComponent<RelicSlot>(); // Set the original parent (RelicSlot)
            }

            GameObject instantiatedRelic = QuickSortSortingGameManager.Instance.PutRelicPart(inventorySlot, relic);
            beforeCheckedSlotsList[instantiatedRelic.GetComponent<StorySegment>().order].GetComponent<RelicCheckedSlot>().correctRelic = instantiatedRelic;
        }

        foreach (GameObject relic in afterRelics)
        {
            GameObject inventorySlot = Instantiate(inventorySlotPrefab, afterInventorySlots.transform);

            relic.GetComponent<RelicMovement>().isMovable = true;
            if (relic.transform.parent.GetComponent<RelicSlot>() != null)
            {
                relic.GetComponent<RelicMovement>().originalParent = transform.parent.GetComponent<RelicSlot>(); // Set the original parent (RelicSlot)
            }

            GameObject instantiatedRelic = QuickSortSortingGameManager.Instance.PutRelicPart(inventorySlot, relic);
            afterCheckedSlotsList[instantiatedRelic.GetComponent<StorySegment>().order - (QuickSortSortingGameManager.Instance.pivotRandIndex + 1)].GetComponent<RelicCheckedSlot>().correctRelic = instantiatedRelic;
        }

        SortingGameManager.Instance.allRelics.AddRange(beforeRelics);
        SortingGameManager.Instance.allRelics.AddRange(afterRelics);

        canvas.transform.position = leftCanvasPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeCanvasPos)
        {
            elapsedTime += Time.deltaTime;
            movePercentage = elapsedTime / canvasMoveDuration;

            if (isInLeftScreen)
            {
                canvas.transform.position = Vector3.Lerp(canvas.transform.position, rightCanvasPos, Mathf.SmoothStep(0, 1, movePercentage));
            }
            else
            {
                canvas.transform.position = Vector3.Lerp(canvas.transform.position, leftCanvasPos, Mathf.SmoothStep(0, 1, movePercentage));
            }
        }

        if (movePercentage >= 1f)
        {
            movePercentage = 0f;
            elapsedTime = 0f;
            changeCanvasPos = false;
            isInLeftScreen = !isInLeftScreen;

            if (isInLeftScreen)
            {
                rightButton.SetActive(true);
            }
            else
            {
                leftButton.SetActive(true);
            }
        }
    }

    public void ButtonPress()
    {
        movePercentage = 0f;
        changeCanvasPos = true;
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

}
