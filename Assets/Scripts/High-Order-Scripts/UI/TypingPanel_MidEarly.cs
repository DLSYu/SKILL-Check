using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum writingStyle_MidEarly { freeform, swbst }

public class TypingPanel_MidEarly : MonoBehaviour
{
    [SerializeField] private GameObject freeFormPanel, swbstPanel;
    [SerializeField] private GameObject storyPanel, gemsPanel, storyAndGemsPanel;
    [SerializeField] private InventoryManager_MidEarly inventoryManager;
    [SerializeField] private TextMeshProUGUI storyText, gemsText;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField[] swbstInputs;

    [SerializeField] private SWBSTSlot_MidEarly[] swbstSlots;

    private writingStyle currentWritingStyle;

    public void OnEnable()
    {
        // Refresh slots whenever panel is opened
        ShowStoryAndGems();
    }

    void Start()
    {
        swbstPanel.SetActive(true);

        // For SWBST inputs
        foreach (var input in swbstInputs)
        {
            input.onSelect.AddListener((s) => {
                if (input.placeholder != null)
                    input.placeholder.gameObject.SetActive(false);
            });

            input.onDeselect.AddListener((s) => {
                if (input.placeholder != null && string.IsNullOrEmpty(input.text))
                    input.placeholder.gameObject.SetActive(true);
            });
        }
    }

    public void ToggleWriting()
    {
        currentWritingStyle = writingStyle.swbst;
        swbstPanel.SetActive(true);
    }

    public writingStyle GetCurrentWritingStyle() => currentWritingStyle;

    public void ShowStoryAndGems()
    {
        //storyPanel.SetActive(true);
        //gemsPanel.SetActive(true);
        //storyAndGemsPanel.SetActive(true);
        storyText.text = StoryData.GetStoryString();

        string tempText = "";
        foreach (Gem_MidEarly gem in inventoryManager.GetGems())
        {
            tempText += $"{gem.GemDescription}\n\n";
        }
        gemsText.text = tempText;

        // Slot validation
        // Get collected types using direct enum conversion
        var collectedTypes = new HashSet<Gem_MidEarly.GemType>();
        foreach (var gem in inventoryManager.GetGems())
        {
            collectedTypes.Add(gem.Type);
        }

        foreach (var slot in swbstSlots)
        {
            bool hasGem = inventoryManager.GetGems().Exists(g =>
                (int)g.Type == (int)slot.slotType
            );

            Debug.Log($"Checking slot: {slot.slotType} | HasGem: {hasGem}");
            slot.SetInteractable(hasGem);
            Debug.Log($"Slot {slot.slotType} enabled: {hasGem} | Gems: {string.Join(", ", inventoryManager.GetGems().ConvertAll(g => g.Type.ToString()))}");
        }
    }

    public void HideStoryAndGems()
    {
        storyPanel.SetActive(false);
        gemsPanel.SetActive(false);
        storyAndGemsPanel.SetActive(false);
    }
}
