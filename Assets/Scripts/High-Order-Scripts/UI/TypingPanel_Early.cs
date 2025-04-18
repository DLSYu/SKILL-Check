using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum writingStyle_Early { freeform, swbst }

public class TypingPanel_Early : MonoBehaviour
{
    [SerializeField] private GameObject freeFormPanel, swbstPanel;
    [SerializeField] private GameObject storyPanel, gemsPanel, storyAndGemsPanel;
    [SerializeField] private InventoryManager_Early inventoryManager;
    [SerializeField] private TextMeshProUGUI storyText, gemsText;

    private writingStyle currentWritingStyle;

    public void ToggleWriting()
    {
        currentWritingStyle = (currentWritingStyle == writingStyle.freeform) ?
            writingStyle.swbst : writingStyle.freeform;

        swbstPanel.SetActive(currentWritingStyle == writingStyle.swbst);
        freeFormPanel.SetActive(currentWritingStyle == writingStyle.freeform);
    }

    public writingStyle GetCurrentWritingStyle() => currentWritingStyle;

    public void ShowStoryAndGems()
    {
        storyPanel.SetActive(true);
        gemsPanel.SetActive(true);
        storyAndGemsPanel.SetActive(true);
        storyText.text = StoryData.GetStoryString();

        string tempText = "";
        foreach (Gem_Early gem in inventoryManager.GetGems())
        {
            tempText += $"{gem.GemDescription}\n\n";
        }
        gemsText.text = tempText;
    }

    public void HideStoryAndGems()
    {
        storyPanel.SetActive(false);
        gemsPanel.SetActive(false);
        storyAndGemsPanel.SetActive(false);
    }
}

