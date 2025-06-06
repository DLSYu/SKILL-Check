using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MidMid : MonoBehaviour
{
    [SerializeField] private GameObject JoystickCanvas;
    [SerializeField] private GameObject TypingCanvas;
    [SerializeField] private GameObject GemCanvas;
    [SerializeField] private GameObject MenuAnimationHandler;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI InventoryText;
    [SerializeField] private Image gemUIImage;
    [SerializeField] private TextMeshProUGUI gemTypeText;
    [SerializeField] private TMPro.TextMeshProUGUI gemTMProDescription;
    [SerializeField] private TMPro.TextMeshProUGUI keywordText;
    [SerializeField] private DoorManager doorManager;
    [SerializeField] private InventoryManager_MidMid inventoryManager;
    [SerializeField] private LoadingScreen loadingScreen;
    public bool isScorePanelCleanable = false;

    private void Awake()
    {
        Screen.SetResolution(2000, 1200, true);
    }

    private void Start()
    {
        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics = new HighOrderStageTypeAnalytics(StageType.Mid);
    }

    public void openTypingScreen()
    {
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

        // if (doorManager.GetCurrentDoor().checkIfKeywordUnlocked())
        // {
        //     keywordText.text = "Keyword: " + doorManager.GetCurrentDoor().keyWord;
        // }
        // else
        // {
        //     keywordText.text = "Keyword: ???";
        // }
    }

    public void exitTypingScreen()
    {
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);

        if (isScorePanelCleanable)
        {
            doorManager.clearScorePanel();
        }
    }

    public void openGemCanvas(string gemDescription, Sprite gemImage, Gem_MidMid.GemType gemType)
    {
        this.gemTMProDescription.text = gemDescription;
        this.gemUIImage.sprite = gemImage;
        this.gemTypeText.text = gemType.ToString();
        GemCanvas.SetActive(true);
        JoystickCanvas.SetActive(false);
    }

    public void exitGemCanvas()
    {
        GemCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    public void openMenu()
    {
        //pause game
        //deactivate control canvas
        //open menu to go back to main menu

        Time.timeScale = 0;
        JoystickCanvas.SetActive(false);
        MenuAnimationHandler.SetActive(true);
        // Menu Canvas set active handled by UIAnimator


    }

    public void exitMenu()
    {
        //resume game
        //activate control canvas
        //close menu
        Time.timeScale = 1;

        MenuAnimationHandler.SetActive(false);
        MenuCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    public void openInventory()
    {
        //open inventory
        //deactivate control canvas
        JoystickCanvas.SetActive(false);
        InventoryCanvas.SetActive(true);

        // get inventory canvas's scroll view
        // put panel and text for each gem in the inventory
        List<Gem_MidMid> gemList = inventoryManager.getGems();
        string tempText = "";

        foreach (Gem_MidMid gem in gemList)
        {
            // get gemData
            string[] currentGemData = gem.getGemData();
            tempText += currentGemData[0] + " - " + currentGemData[1] + "\n\n";
        }

        InventoryText.text = tempText;
    }

    public void exitInventory()
    {
        //close inventory
        //activate control canvas
        InventoryCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    // Status Checkers
    public bool isTypingScreenOpen()
    {
        return TypingCanvas.activeSelf;
    }
    public bool isJoystickScreenOpen()
    {
        return JoystickCanvas.activeSelf;
    }

    // Other Functions

    public void quitStage()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        loadingScreen.LoadScene("Lobby");
    }
}
