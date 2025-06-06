using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Gets gem straightforwardly
public class Gem_Late : GemInterface
{
    public override void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.IncrementGemsCollected();
        gameObject.SetActive(false);
        uiManager.openGemCanvas(gemDescription, gemType.ToString(), gemName);
        inventoryManager.addGemToInventory(this);
    }

}