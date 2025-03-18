using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInteractable
{
    [SerializeField] private Door door;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private String gemName;
    [SerializeField] private String gemDescription;

    public void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        gameObject.SetActive(false);
        uiManager.openGemCanvas(gemDescription, gemName);
        door.collectGem();
        inventoryManager.addGemToInventory(this);
    }

    public string[] getGemData()
    {
        string[] gemData = { gemName, gemDescription };
        return gemData;
    }
}
