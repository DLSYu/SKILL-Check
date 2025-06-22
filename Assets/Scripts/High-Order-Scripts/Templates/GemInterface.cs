using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInterface : MonoBehaviour, IInteractable
{
    [Header("Gem Details")]
    [SerializeField] protected String gemName;
    [SerializeField] protected GemType gemType;
    [SerializeField] protected string gemDescription;
    [SerializeField] protected bool isColorless;
    [Header("References")]
    [SerializeField] protected AudioClip gemSound;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected InventoryManager inventoryManager;
    [SerializeField] protected UIManagerTemplate uiManager;
    // For Highlighting in HO_Early
    [Header("Keyword")]
    [SerializeField] protected string keyword;
    public string Keyword => keyword;
    /*** 
    * Allowed Gem Names and corresponding SWBST Mapping:
        * Somebody = Character
        * Wanted   = Problem, Initiating Event
        * But      = Problem, Initiating Event
        * So       = Internal Response, Plan, Attempt / Action
        * Then     = Consequence, Resolution / Resolution
    ***/

    public GemType Type => gemType;
    public string GemDescription => gemDescription;
    public string GemName => gemName;

    public enum GemType
    {
        Somebody,
        Wanted,
        But,
        So,
        Then
    }

    void Awake()
    {
        if (isColorless)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color32(19, 19, 19, 255);
        }
    }
    public virtual void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        gameObject.SetActive(false);
    }

    public virtual string[] getGemData()
    {
        string[] gemData = { gemName, gemDescription, gemType.ToString(), isColorless.ToString() };
        return gemData;
    }

    // Methods for gem comparisons
    public bool compareGemType(GemInterface otherGem)
    {
        if (otherGem.gemType == this.gemType)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool checkIfGemTypeInList(List<GemInterface> gemList)
    {
        foreach (GemInterface gem in gemList)
        {
            if (compareGemType(gem))
            {
                return true;
            }
        }
        return false;
    }

    public GemInterface getGemFromGemType(List<GemInterface> gemList)
    {
        foreach (GemInterface gem in gemList)
        {
            if (compareGemType(gem))
            {
                return gem;
            }
        }
        return null;
    }

    public void copyGemData(GemInterface otherGem)
    {
        this.gemType = otherGem.Type;
        this.gemDescription = otherGem.GemDescription;
    }

}