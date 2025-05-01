using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GemType mappedSWBST;
    [SerializeField] private String gemName;
    /*** 
    * Allowed Gem Names and corresponding SWBST Mapping:
        * Somebody = Character
        * Wanted   = Problem, Initiating Event
        * But      = Problem, Initiating Event
        * So       = Internal Response, Plan, Attempt / Action
        * Then     = Consequence, Resolution / Resolution
    ***/
    [SerializeField] private String gemDescription;

    private enum GemType
    {
        Somebody,
        Wanted,
        But,
        So,
        Then,
    }
    public void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        gameObject.SetActive(false);
        uiManager.openGemCanvas(gemDescription, gemName);
        inventoryManager.addGemToInventory(this);
    }

    public string[] getGemData()
    {
        string[] gemData = { gemName, gemDescription };
        return gemData;
    }
}