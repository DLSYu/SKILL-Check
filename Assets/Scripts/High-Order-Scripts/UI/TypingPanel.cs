using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] List<GemInterface> displayedGems = new List<GemInterface>();

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

    [Header("Mid level input fields")]
    [SerializeField] private TMPro.TMP_InputField somebodyField;
    [SerializeField] private TMPro.TMP_InputField wantedField;
    [SerializeField] private TMPro.TMP_InputField butField;
    [SerializeField] private TMPro.TMP_InputField soField;
    [SerializeField] private TMPro.TMP_InputField thenField;

    private bool isCurrentlyShowingStory = true;

    // General Functions
    public void showStoryAndGems()
    {
        // storyText.text = inventoryManager.GetStory();
        // gemsText.text = inventoryManager.GetGems();
        storyTextContentHolder.GetComponent<TMPro.TextMeshProUGUI>().text = StoryData.GetStoryString();

        List<GemInterface> gemList = inventoryManager.getGems();

        foreach (GemInterface gem in gemList)
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

    // For Early & Mid level Typing Screen
    public void LoadCollectedGems()
    {
        List<GemInterface> collectedGems = inventoryManager.getGems();
        foreach (GemInterface gem in displayedGems)
        {
            if (gem.checkIfGemTypeInList(collectedGems))
            {
                GemInterface correspondingCollectedGem = gem.getGemFromGemType(collectedGems);

                gem.copyGemData(correspondingCollectedGem);
                // Don't show gem if slotted in SWBST already
                GameObject parent = gem.gameObject.transform.parent.gameObject;
                if (parent.gameObject.GetComponent<RelicInventorySlot>() != null)
                {
                    gem.gameObject.SetActive(true);
                }
            }
            else
            {
                gem.gameObject.SetActive(false);
            }
        }
    }

    // For Mid level Typing Screen
    public void LoadTypingPanels()
    {
        List<GemInterface> collectedGems = inventoryManager.getGems();
        foreach (GemInterface gem in collectedGems)
        {
            if (gem.Type == GemInterface.GemType.Somebody)
            {
                somebodyField.interactable = true;
            }
            else if (gem.Type == GemInterface.GemType.Wanted)
            {
                wantedField.interactable = true;
            }
            else if (gem.Type == GemInterface.GemType.But)
            {
                butField.interactable = true;
            }
            else if (gem.Type == GemInterface.GemType.So)
            {
                soField.interactable = true;
            }
            else if (gem.Type == GemInterface.GemType.Then)
            {
                thenField.interactable = true;
            }
        }
    }

    // For Late level Typing Screen
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

    // if a panel is null, return the opposite panel
    public writingStyle GetCurrentWritingStyle()
    {
        if (freeFormPanel == null)
            return writingStyle.swbst;
        else if (swbstPanel == null)
            return writingStyle.freeform;
        else
            return currentWritingStyle;
    }


}
