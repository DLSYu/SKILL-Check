using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : GemInterface
{
    [Header("Specific References")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GemType mappedSWBST;

    public override void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        gameObject.SetActive(false);
        uiManager.openGemCanvas(gemDescription, gemName);
        inventoryManager.addGemToInventory(this);
    }

    public override string[] getGemData()
    {
        string[] gemData = { gemName, gemDescription };
        return gemData;
    }
}