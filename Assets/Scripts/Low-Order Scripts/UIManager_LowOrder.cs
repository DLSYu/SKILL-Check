using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager_LowOrder : MonoBehaviour
{
    [SerializeField] private GameObject MenuAnimationHandler;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private LoadingScreen loadingScreen;

    [SerializeField] private List<GameObject> relicSlotsParent;
    [SerializeField] private List<GameObject> relics;
    [SerializeField] private GameObject beforeStoryAnimatorUI;
    [SerializeField] private GameObject storyUI;

    float analyticsStoryTimer = 0;

    private void Awake()
    {
        Screen.SetResolution(2000, 1200, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomizeStartingPos();
    }

    void RandomizeStartingPos()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        List<GameObject> relicSlotsParentCopy = new List<GameObject>(relicSlotsParent);
        //Debug.Log($"relicSlotsParentCopy.Count: {relicSlotsParentCopy.Count}");


        for (int i = 0; i < relicSlotsParentCopy.Count; i++)
        {
            //Debug.Log($"relicSlotsParentCopy i: {i}");
            int randomizedNum = Random.Range(0, relics.Count - 1 - i);
            //Debug.Log($"remaining slots: {0} - {relics.Count - 1 - i}");

            relicSlotsParentCopy[randomizedNum].GetComponent<RelicSlot>().PlaceRelic(relics[i]);
            relics[i].GetComponent<RelicMovement>().originalParent = relicSlotsParentCopy[randomizedNum].GetComponent<RelicSlot>();
            //Debug.Log($"set original at randomized: {relics[i].name}");

        }
        relicSlotsParentCopy.Clear();

    }

    // Update is called once per frame
    void Update()
    {
        if (storyUI.activeInHierarchy)
        {
            analyticsStoryTimer += Time.deltaTime;
        }
    }

    public void openMenu()
    {
        //pause game
        //open menu to go back to main menu

        Time.timeScale = 0;
        MenuAnimationHandler.SetActive(true);
        // Menu Canvas set active handled by UIAnimator


    }

    public void exitMenu()
    {
        //resume game
        //close menu
        Time.timeScale = 1;

        MenuAnimationHandler.SetActive(false);
        MenuCanvas.SetActive(false);
    }

    public void quitStage()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        loadingScreen.LoadScene("Lobby");
    }

    public void peekStoryButtonClicked()
    {
        // don't pause
        if (!beforeStoryAnimatorUI.activeInHierarchy)
        {

            beforeStoryAnimatorUI.SetActive(true);

        }

    }

    public void showStoryUI()
    {
        beforeStoryAnimatorUI.SetActive(false);
        resetRelics();
        storyUI.SetActive(true);

    }


    public void resetRelics()
    {
        /*
        for (int i = 0; i < relicSlotsParent.Count; i++)
        {


            relicSlotsParent[i].GetComponent<RelicSlot>().PlaceRelic(relics[i]);
            relics[i].GetComponent<RelicMovement>().originalParent = relicSlotsParent[i].GetComponent<RelicSlot>();

        }
        */
        RandomizeStartingPos();
    }

    public void exitStoryUI()
    {
        LowOrderAnalyticsManager.instance.lowOrderStageAnalytics.AddTimeSpentPeekingInStoryList(analyticsStoryTimer);
        analyticsStoryTimer = 0;

        // dismiss ui
        storyUI.SetActive(false);
    }


}
