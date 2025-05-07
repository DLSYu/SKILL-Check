using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine;

public class UIManagerTemplate : MonoBehaviour
{
    public static UIManagerTemplate Instance;
    [SerializeField] protected GameObject JoystickCanvas;
    [SerializeField] protected GameObject TypingCanvas;
    [SerializeField] protected GameObject GemCanvas;
    [SerializeField] private GameObject MenuAnimationHandler;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private TextMeshProUGUI InventoryGemDescriptionText;
    [SerializeField] private TextMeshProUGUI InventoryGemDescriptionType;
    [SerializeField] protected TextMeshProUGUI gemTMProDescription, gemTMProName;
    [SerializeField] private GameObject gemScrollViewContent;
    [SerializeField] private GameObject gemInventoryPrefab;
    [SerializeField] protected TextMeshProUGUI keywordText;
    [SerializeField] protected DoorManager doorManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] protected GateSubmit gateSubmit;
    public bool isScorePanelCleanable = false;
    private List<GameObject> gemInventoryGameObjectList = new List<GameObject>();

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Screen.SetResolution(2000, 1200, true);
    }

    public virtual void openTypingScreen()
    {
        Time.timeScale = 0;
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);
    }

    public virtual void exitTypingScreen()
    {
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void openGemCanvas(String gemDescription, String gemType, String gemName)
    {
        Time.timeScale = 0;
        this.gemTMProDescription.text = gemDescription;
        this.gemTMProName.text = gemName;
        GemCanvas.SetActive(true);
        JoystickCanvas.SetActive(false);
    }

    public void exitGemCanvas()
    {
        GemCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
        Time.timeScale = 1;
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

    public void updateInventoryGemSelectedText(string type, string description)
    {
        InventoryGemDescriptionType.text = type;
        InventoryGemDescriptionText.text = description;
    }

    public void inventoryGemHighlight(int id)
    {
        for (int i = 0; i < gemInventoryGameObjectList.Count; i++)
        {
            if (gemInventoryGameObjectList[i].GetComponent<GemInventoryPrefab>().getId() == id)
                gemInventoryGameObjectList[i].GetComponent<GemInventoryPrefab>().setHighlight(true);
            else
                gemInventoryGameObjectList[i].GetComponent<GemInventoryPrefab>().setHighlight(false);
        }
    }

    public void openInventory()
    {
        Time.timeScale = 0;
        //open inventory
        //deactivate control canvas
        JoystickCanvas.SetActive(false);
        InventoryCanvas.SetActive(true);

        // get inventory canvas's scroll view
        // put panel and text for each gem in the inventory
        List<GemInterface> gemList = inventoryManager.getGems();


        foreach (Transform child in gemScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        gemInventoryGameObjectList.Clear();

        int id = 0;
        foreach (GemInterface gem in gemList)
        {
            // get gemData
            string[] currentGemData = gem.getGemData();
            GameObject newGemPrefab = Instantiate(gemInventoryPrefab, gemScrollViewContent.transform);
            newGemPrefab.GetComponent<GemInventoryPrefab>().setType(currentGemData[0]);
            newGemPrefab.GetComponent<GemInventoryPrefab>().setDescription(currentGemData[1]);
            newGemPrefab.GetComponent<GemInventoryPrefab>().setId(id);
            gemInventoryGameObjectList.Add(newGemPrefab);
            newGemPrefab.SetActive(true);
            id++;
        }
        if (gemList.Count == 0)
        {
            InventoryGemDescriptionType.text = "";
            InventoryGemDescriptionText.text = "No gem collected yet!";
        }
        else
        {
            InventoryGemDescriptionType.text = gemList[0].getGemData()[0];
            InventoryGemDescriptionText.text = gemList[0].getGemData()[1];
            inventoryGemHighlight(0);
        }
    }

    public void exitInventory()
    {
        Time.timeScale = 1;
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
