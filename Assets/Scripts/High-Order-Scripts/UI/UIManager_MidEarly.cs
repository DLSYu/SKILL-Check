using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MidEarly : MonoBehaviour
{
    [SerializeField] private GameObject JoystickCanvas;
    [SerializeField] private GameObject TypingCanvas;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private TextMeshProUGUI InventoryText;
    [SerializeField] private TextMeshProUGUI keywordText;
    [SerializeField] private DoorManager_MidEarly doorManager;
    [SerializeField] private InventoryManager_MidEarly inventoryManager;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private GameObject GemCanvas;
    [SerializeField] private TextMeshProUGUI gemDescriptionText;
    // Side-by-Side
    [SerializeField] private TextMeshProUGUI gemStoryGrammarLabelText;
    [SerializeField] private Image gemDisplayImage;

    public bool isScorePanelCleanable = false;
    private Image GemLabel;

    void Awake()
    {
        Screen.SetResolution(2000, 1200, true);
        if (GemLabel == null)
        {
            var gemCanvas = GameObject.Find("GemPanel");
            if (gemCanvas != null) GemLabel = gemCanvas.GetComponentInChildren<Image>(true);
        }
        if (gemDisplayImage == null) Debug.LogError("Assign gemDisplayImage in Inspector!");
    }

    public void OpenTypingScreen()
    {
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

        FindObjectOfType<TypingPanel_MidEarly>().ShowStoryAndGems();

        Door_MidEarly currentDoor = doorManager.GetCurrentDoor() as Door_MidEarly;
        if (currentDoor != null)
        {
            keywordText.text = currentDoor.CheckIfKeywordUnlocked() ?
                "Keyword: " + currentDoor.keyWord : "Keyword: ???";
        }
    }

    public void ExitTypingScreen()
    {
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
        if (isScorePanelCleanable) doorManager.ClearScorePanel();
    }

    public void ShowGemDetails(Gem_MidEarly gem)
    {
        GemCanvas.transform.SetAsLastSibling();

        if (gemDisplayImage != null)
        {
            gemDisplayImage.sprite = gem.GemImage;
            gemDisplayImage.preserveAspect = true;
        }
        gemDescriptionText.text = gem.GemDescription;
        gemStoryGrammarLabelText.text = gem.GemStoryGrammarLabel;

        // Force layout rebuild
        LayoutRebuilder.ForceRebuildLayoutImmediate(GemCanvas.GetComponent<RectTransform>());
        GemCanvas.SetActive(true);

        // Debugging check
        Debug.Log($"Showing gem popup for: {gem.Type}");
    }

    public void ExitGemCanvas() => GemCanvas.SetActive(false);

    public void OpenMenu()
    {
        Time.timeScale = 0;
        MenuCanvas.SetActive(true);
        JoystickCanvas.SetActive(false);
    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        MenuCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    public void OpenInventory()
    {
        JoystickCanvas.SetActive(false);
        InventoryCanvas.SetActive(true);
    }

    public void ExitInventory()
    {
        InventoryCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    public bool IsTypingScreenOpen() => TypingCanvas.activeSelf;
    public bool IsJoystickScreenOpen() => JoystickCanvas.activeSelf;

    public void QuitStage()
    {
        if (Time.timeScale == 0) Time.timeScale = 1;
        loadingScreen.LoadScene("Lobby");
    }

    public void RefreshTypingPanel()
    {
        var typingPanel = TypingCanvas.GetComponent<TypingPanel_MidEarly>();
        if (typingPanel != null)
        {
            typingPanel.ShowStoryAndGems();
        }
    }
}
