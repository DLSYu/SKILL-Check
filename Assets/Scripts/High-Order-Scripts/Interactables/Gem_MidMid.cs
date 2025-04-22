using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_MidMid : MonoBehaviour, IInteractable
{
    [Header("Gem Details")]
    [SerializeField] private Sprite gemImage;
    [SerializeField] private GemType gemType;
    [SerializeField] private string gemDescription;

    [Header("References")]
    [SerializeField] private Door_MidMid door;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager_MidMid uiManager;
    [SerializeField] private InventoryManager_MidMid inventoryManager;

    public enum GemType { Somebody, Wanted, But, So, Then }
    public Sprite GemImage => gemImage;
    public GemType Type => gemType;
    public string GemDescription => gemDescription;

    public void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        gameObject.SetActive(false);
        uiManager.openGemCanvas(gemDescription, gemImage, gemType);
        door.collectGem();
        inventoryManager.addGemToInventory(this);
    }

    public string[] getGemData()
    {
        return new string[] { gemDescription, gemType.ToString() };
        //return gemData;
    }
}
