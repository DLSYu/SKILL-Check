using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SWBSTSlot : MonoBehaviour
{
    public enum SlotType { Somebody, Wanted, But, So, Then }

    [Header("SWBST Configuration")]
    [SerializeField] private SlotType slotType;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI placeholderText;
    [SerializeField] private Button resetButton;

    [Header("Gem Tracking")]
    private Transform originalParent;
    private Gem_Early currentGem;
    private Vector3 originalGemPosition;

    [SerializeField] private GameObject relicPopupPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        relicPopupPanel.SetActive(true);
    }

    void Start()
    {
        resetButton.onClick.AddListener(ResetSlot);
        UpdatePlaceholderVisibility();
    }
    void Update()
    {
        //Debug.Log($"Collected Gems: {gems.Count}");
        Debug.Log($"Slot {slotType} has gem: {currentGem != null}");
        Debug.Log($"Slot {slotType} status: {(currentGem != null ? "Occupied" : "Empty")}");
    }

    // Called when a gem is dropped onto this slot
    public bool TryPlaceGem(Gem_Early gem)
    {
        if (gem == null)
        {
            Debug.Log("Tried to place null gem!");
            return false;
        }

        // Check if slot already has a gem
        if (currentGem != null)
        {
            Debug.Log("Slot already occupied!");
            return false;
        }

        // Store original parent and position
        originalParent = gem.transform.parent;
        // Hide the gem
        gem.gameObject.SetActive(false);

        // Update UI
        displayText.text = gem.GemDescription;
        placeholderText.gameObject.SetActive(false);

        // Move gem to slot
        gem.transform.SetParent(transform);
        gem.transform.localPosition = Vector3.zero;

        // Disable gem's raycast after placement
        CanvasGroup gemCanvasGroup = gem.GetComponent<CanvasGroup>();
        if (gemCanvasGroup != null)
        {
            gemCanvasGroup.blocksRaycasts = false;
        }

        // Force update TextMeshPro components
        if (displayText != null)
        {
            displayText.text = gem.GemDescription;
            displayText.ForceMeshUpdate(); // Add this line
        }

        if (placeholderText != null)
        {
            placeholderText.gameObject.SetActive(false);
            placeholderText.ForceMeshUpdate(); // Add this line
        }

        currentGem = gem;
        InventoryManager_Early.Instance.MoveToSWBST(gem);
        Debug.Log($"Placed {gem.Type} in {slotType} slot");
        return true;
    }

    // Called when the reset button is clicked
    public void ResetSlot()
    {
        if (currentGem == null) return;

        // Return to ORIGINAL parent (not slot's inventory)
        currentGem.transform.SetParent(originalParent);
        currentGem.transform.localPosition = Vector3.zero; // Reset position
        currentGem.gameObject.SetActive(true);

        // Enable interaction
        currentGem.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Force UI update
        displayText.text = "";
        placeholderText.gameObject.SetActive(true);
        displayText.ForceMeshUpdate();
        placeholderText.ForceMeshUpdate();

        InventoryManager_Early.Instance.ReturnFromSWBST(currentGem);
        currentGem = null;
    }

    private void UpdatePlaceholderVisibility()
    {
        placeholderText.gameObject.SetActive(currentGem == null);
    }
}