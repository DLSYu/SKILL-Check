using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_Early : MonoBehaviour
{
    public static UIManager_Early Instance;
    [SerializeField] private GameObject JoystickCanvas;
    [SerializeField] private GameObject TypingCanvas;
    [SerializeField] private GameObject GemCanvas;
    [SerializeField] private GameObject MenuAnimationHandler;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private TextMeshProUGUI InventoryGemDescriptionText;
    [SerializeField] private TextMeshProUGUI InventoryGemDescriptionType;
    [SerializeField] private TextMeshProUGUI gemTMProDescription, gemTMProName;
    [SerializeField] private GameObject gemScrollViewContent;
    [SerializeField] private GameObject gemInventoryPrefab;
    [SerializeField] private TextMeshProUGUI keywordText;
    [SerializeField] private DoorManager_Early doorManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GateSubmit gateSubmit;
    public bool isScorePanelCleanable = false;
    private List<GameObject> gemInventoryGameObjectList = new List<GameObject>();
    private Image GemLabel;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Screen.SetResolution(2000, 1200, true);
        if (GemLabel == null)
        {
            var gemCanvas = GameObject.Find("GemPanel");
            if (gemCanvas != null) GemLabel = gemCanvas.GetComponentInChildren<Image>(true);
        }
        //if (gemDisplayImage == null) Debug.LogError("Assign gemDisplayImage in Inspector!");
    }

    public void openTypingScreen()
    {
        Time.timeScale = 0;
        TypingCanvas.GetComponent<TypingPanel>().LoadCollectedGems();
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

    }

    public void exitTypingScreen()
    {
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
        // if (isScorePanelCleanable) doorManager.ClearScorePanel();
        Time.timeScale = 1;
    }

    public void openGemCanvas(String gemDescription, String gemType, String gemName)
    {
        Time.timeScale = 0;
        this.gemTMProName.text = gemName;
        this.gemTMProDescription.text = gemDescription;
        //Set Gem type
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
        Time.timeScale = 0;
        JoystickCanvas.SetActive(false);
        MenuAnimationHandler.SetActive(true);
    }

    public void exitMenu()
    {
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

        foreach (Gem_Early gem in gemList)
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

    public bool IsTypingScreenOpen() => TypingCanvas.activeSelf;
    public bool IsJoystickScreenOpen() => JoystickCanvas.activeSelf;

    public void quitStage()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        loadingScreen.LoadScene("Lobby");
    }
}
