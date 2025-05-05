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
    [SerializeField] List<Gem_Early> displayedGems = new List<Gem_Early>();

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
    void Start()
    {
        // foreach (Gem_Early gem in displayedGems)
        // {
        //     gem.gameObject.SetActive(false);
        // }
        // Gem_Early Somebody = new Gem_Early(Gem_Early.GemType.Somebody, "");
        // Gem_Early Wanted = new Gem_Early(Gem_Early.GemType.Wanted, "");
        // Gem_Early But = new Gem_Early(Gem_Early.GemType.But, "");
        // Gem_Early So = new Gem_Early(Gem_Early.GemType.So, "");
        // Gem_Early Then = new Gem_Early(Gem_Early.GemType.Then, "");
    }
    public void LoadCollectedGems()
    {
        List<GemInterface> collectedGems = inventoryManager.getGems();
        foreach (Gem_Early gem in displayedGems)
        {
            if (gem.checkIfGemTypeInList(collectedGems))
            {
                Gem_Early correspondingCollectedGem = gem.getGemFromGemType(collectedGems);

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
}
