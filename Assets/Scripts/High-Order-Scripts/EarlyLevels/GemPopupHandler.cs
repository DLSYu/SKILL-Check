using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GemPopupHandler : MonoBehaviour
{
    public GameObject relicPopupPanel; // Reference to the pop-up panel
    public TextMeshProUGUI relicText; // Reference to the text component
    public string passage; // The passage to display

    public Gem_MidMid.GemType requiredGemType;

    private bool isPopupActive = false;

    void Start()
    {
        // Ensure the pop-up is hidden at the start
        if (relicPopupPanel != null)
        {
            relicPopupPanel.SetActive(false);
        }
    }

    // Call this method when the relic is tapped
    public void OnRelicTapped()
    {
        // Check if the player has collected the required gem
        InventoryManager_MidMid inventory = FindObjectOfType<InventoryManager_MidMid>();
        bool hasGem = inventory.getGems().Any(gem => gem.Type == requiredGemType);

        if (!isPopupActive && hasGem)
        {
            // Show the pop-up and set the passage text
            relicPopupPanel.SetActive(true);
            relicText.text = passage;
            isPopupActive = true;
        }
    }

    // Call this method when the close button is clicked
    public void OnCloseButtonClicked()
    {
        if (isPopupActive)
        {
            // Hide the pop-up
            relicPopupPanel.SetActive(false);
            isPopupActive = false;
        }
    }
}
