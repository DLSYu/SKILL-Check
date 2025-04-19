using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gem_MidEarly : MonoBehaviour, IInteractable
{
    [Header("Gem Details")]
    [SerializeField] private Sprite gemImage;
    [SerializeField] private GemType gemType;
    [SerializeField] private string gemDescription;
    [SerializeField] private string gemStoryGrammarLabel;

    [Header("References")]
    [SerializeField] private Door_MidEarly door;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager_MidEarly uiManager;
    [SerializeField] private InventoryManager_MidEarly inventoryManager;

    public enum GemType { Somebody, Wanted, But, So, Then }
    public Sprite GemImage => gemImage;
    public GemType Type => gemType;
    public string GemDescription => gemDescription;
    public string GemStoryGrammarLabel => gemStoryGrammarLabel;

    public void Interact()
    {
        if (door == null || inventoryManager == null || uiManager == null)
        {
            Debug.LogError("Missing references in Gem_MidEarly!");
            return;
        }

        audioSource.PlayOneShot(gemSound);
        uiManager.ShowGemDetails(this);

        Debug.Log($"Interacting with {Type} gem at {Time.time}");

        // Check tag instead of parent
        if (gameObject.CompareTag("Interactable"))
        {
            gameObject.SetActive(false);
            door.CollectGem();
            inventoryManager.AddGemToInventory(this);

            // Refresh UI immediately
            uiManager.RefreshTypingPanel();
        }
    }
}
