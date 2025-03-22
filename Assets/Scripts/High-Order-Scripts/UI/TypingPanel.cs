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
    private GameObject storyPanel, gemsPanel, storyAndGemsPanel;
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private TMPro.TextMeshProUGUI storyText, gemsText;
    private writingStyle currentWritingStyle;

    public void ToggleWriting()
    {
        if (currentWritingStyle == writingStyle.freeform)
        {
            currentWritingStyle = writingStyle.swbst;
            swbstPanel.SetActive(true);
            freeFormPanel.SetActive(false);
        }
        else
        {
            currentWritingStyle = writingStyle.freeform;
            freeFormPanel.SetActive(true);
            swbstPanel.SetActive(false);
        }
    }

    public writingStyle GetCurrentWritingStyle()
    {
        return currentWritingStyle;
    }

    public void showStoryAndGems()
    {
        storyPanel.SetActive(true);
        gemsPanel.SetActive(true);
        storyAndGemsPanel.SetActive(true);
        // storyText.text = inventoryManager.GetStory();
        // gemsText.text = inventoryManager.GetGems();
        storyText.text = StoryData.GetStoryString();
        List<Gem> gemList = inventoryManager.getGems();
        string tempText = "";

        foreach (Gem gem in gemList)
        {
            // get gemData
            string[] currentGemData = gem.getGemData();
            tempText += currentGemData[0] + " - " + currentGemData[1] + "\n\n";
        }

        gemsText.text = tempText;
    }

    public void hideStoryAndGems()
    {
        storyPanel.SetActive(false);
        gemsPanel.SetActive(false);
        storyAndGemsPanel.SetActive(false);

    }

}
