using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum writingStyle
{
    freeform,
    swbst
}

public class TypingPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject freeFormPanel, swbstPanel;
    [SerializeField]
    private InventoryManager inventoryManager;

    private writingStyle currentWritingStyle;

    [SerializeField] private TMPro.TextMeshProUGUI freeformOrSWBSTTitleText;


    [Header("Story and Inventory Panel")]
    [SerializeField]
    private GameObject storyAndInventoryPanelHolder;

    [SerializeField]
    private GameObject storyTextContentHolder;

    [SerializeField]
    private GameObject storyTextObjectHolder;

    [SerializeField]
    private GameObject gemPrefab;
    [SerializeField]
    private GameObject gemScrollviewContentHolder;
    [SerializeField]
    private TMPro.TextMeshProUGUI storyAndInventoryTitleText;
    [SerializeField]
    private GameObject gemScrollview;
    [SerializeField]
    private GameObject inventoryButton;
    [SerializeField]
    private GameObject storyButton;




    private bool isCurrentlyShowingStory = true;

    public void ToggleWriting()
    {
        if (currentWritingStyle == writingStyle.freeform)
        {
            currentWritingStyle = writingStyle.swbst;
            swbstPanel.SetActive(true);
            freeFormPanel.SetActive(false);
            freeformOrSWBSTTitleText.text = "SWBST";
        }
        else
        {
            currentWritingStyle = writingStyle.freeform;
            freeFormPanel.SetActive(true);
            swbstPanel.SetActive(false);
            freeformOrSWBSTTitleText.text = "Freeform";
        }
    }

    public writingStyle GetCurrentWritingStyle()
    {
        return currentWritingStyle;
    }

    public void showStoryAndGems()
    {
        // storyText.text = inventoryManager.GetStory();
        // gemsText.text = inventoryManager.GetGems();
        storyTextContentHolder.GetComponent<TMPro.TextMeshProUGUI>().text = StoryData.GetStoryString();
        List<Gem> gemList = inventoryManager.getGems();

        foreach (Gem gem in gemList)
        {
            // get gemData
            string[] currentGemData = gem.getGemData();
            GameObject newGemPrefab = Instantiate(gemPrefab, gemScrollviewContentHolder.transform);
            newGemPrefab.GetComponent<GemSummarizationScrollviewPrefab>().setGemType(currentGemData[0]);
            newGemPrefab.GetComponent<GemSummarizationScrollviewPrefab>().setGemDescription(currentGemData[1]);
            newGemPrefab.SetActive(true);

        }

        storyAndInventoryPanelHolder.SetActive(true);



    }

    public void swapStoryAndInventory()
    {
        if (isCurrentlyShowingStory)
        {
            storyButton.SetActive(true);
            storyTextObjectHolder.SetActive(false);
            inventoryButton.SetActive(false);
            gemScrollview.SetActive(true);
            isCurrentlyShowingStory = false;
            storyAndInventoryTitleText.text = "Inventory";
        }
        else
        {
            storyButton.SetActive(false);
            storyTextObjectHolder.SetActive(true);
            inventoryButton.SetActive(true);
            gemScrollview.SetActive(false);
            isCurrentlyShowingStory = true;
            storyAndInventoryTitleText.text = "Story";
        }

    }

    public void hideStoryAndGems()
    {

        foreach (Transform child in gemScrollviewContentHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        storyAndInventoryPanelHolder.SetActive(false);

    }
}
