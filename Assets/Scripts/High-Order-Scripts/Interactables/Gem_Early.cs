using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Early : MonoBehaviour, IInteractable
{
    [Header("Gem Details")]
    [SerializeField] private string gemName;
    [SerializeField] private Sprite gemImage;
    [SerializeField] private GemType gemType;
    [SerializeField] private string gemDescription;

    [Header("References")]
    [SerializeField] private Door_Early door;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager_Early uiManager;
    [SerializeField] private InventoryManager_Early inventoryManager;

    public enum GemType { Somebody, Wanted, But, So, Then }
    public Sprite GemImage => gemImage;
    public GemType Type => gemType;
    public string GemDescription => gemDescription;
    public string GemName => gemName;
    /*** 
    * Allowed Gem Names and corresponding SWBST Mapping:
        * Somebody = Character
        * Wanted   = Problem, Initiating Event
        * But      = Problem, Initiating Event
        * So       = Internal Response, Plan, Attempt / Action
        * Then     = Consequence, Resolution / Resolution
    ***/
    public void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        uiManager.openGemCanvas(gemDescription, gemType.ToString(), gemName);

        if (transform.parent == null || transform.parent.GetComponent<SWBSTSlot>() == null)
        {
            gameObject.SetActive(false);
            // door.CollectGem();
            inventoryManager.AddGemToInventory(this);
        }
    }

    public string[] getGemData()
    {
        string[] gemData = { gemType.ToString(), gemDescription };
        return gemData;
    }

    //--------------- METHODS FOR EARLY LEVEL GEM MATCHING MECHANIC ------------------//

    public bool compareGemType(Gem_Early otherGem)
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

    public bool checkIfGemTypeInList(List<Gem_Early> gemList)
    {
        foreach (Gem_Early gem in gemList)
        {
            if (compareGemType(gem))
            {
                return true;
            }
        }
        return false;
    }

    public Gem_Early getGemFromGemType(List<Gem_Early> gemList)
    {
        foreach (Gem_Early gem in gemList)
        {
            if (compareGemType(gem))
            {
                return gem;
            }
        }
        return null;
    }

    public void copyGemData(Gem_Early otherGem)
    {
        this.gemType = otherGem.gemType;
        this.gemDescription = otherGem.gemDescription;
    }
}
