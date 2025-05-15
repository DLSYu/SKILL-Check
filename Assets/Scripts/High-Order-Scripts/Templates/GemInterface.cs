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
    [Header("References")]
    [SerializeField] protected AudioClip gemSound;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected InventoryManager inventoryManager;
    [SerializeField] protected UIManagerTemplate uiManager;
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
        Then,
    }
    public virtual void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        gameObject.SetActive(false);
    }

    public virtual string[] getGemData()
    {
        string[] gemData = { gemName, gemDescription, gemType.ToString() };
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