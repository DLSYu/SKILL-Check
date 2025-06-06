using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gem_MidEarly : GemInterface
{
    // [Header("Specific References")]
    // [SerializeField] private InventoryManager_MidEarly inventoryManager;



    public override void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.IncrementGemsCollected();
        gameObject.SetActive(false);
        uiManager.openGemCanvas(gemDescription, gemType.ToString(), gemName);
        inventoryManager.addGemToInventory(this);
        // Refresh UI Typing Panel?
    }
}
