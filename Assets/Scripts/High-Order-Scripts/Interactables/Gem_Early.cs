using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem_Early : MonoBehaviour, IInteractable
{
    [Header("Gem Details")]
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

    public void Interact()
    {
        audioSource.PlayOneShot(gemSound);
        uiManager.ShowGemDetails(this);

        if (transform.parent == null || transform.parent.GetComponent<SWBSTSlot>() == null)
        {
            gameObject.SetActive(false);
            door.CollectGem();
            inventoryManager.AddGemToInventory(this);
        }
    }
}
