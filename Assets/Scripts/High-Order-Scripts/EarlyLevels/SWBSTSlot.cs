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
    private bool isCorrect = false;

    [Header("Gem Tracking")]
    private Transform originalParent;
    [SerializeField]
    private Gem_Early currentGem;
    private Vector3 originalGemPosition;

    [SerializeField] private GameObject relicPopupPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        relicPopupPanel.SetActive(true);
    }

    void Start()
    {
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetSlot);
        UpdatePlaceholderVisibility();

        // Initialize reset button visibility
        isCorrect = false;
        UpdateResetButtonVisibility();
    }
    void Update()
    {
        //Debug.Log($"Collected Gems: {gems.Count}");
        //Debug.Log($"Slot {slotType} has gem: {currentGem != null}");
        Debug.Log($"Slot {slotType} status: {(currentGem != null ? "Occupied" : "Empty")}");

        if (resetButton != null)
        {
            if (currentGem == null)
                resetButton.interactable = false;
            else
                resetButton.interactable = true;
        }
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

        // InventoryManager_Early.Instance.MoveToSWBST(gem);
        Debug.Log($"Placed {gem.Type} in {slotType} slot");

        return true;
    }

    // Called when the reset button is clicked
    public void ResetSlot()
    {
        if (currentGem == null) return;

        Debug.Log($"Resetting slot {slotType} with gem: {currentGem.name}");

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

        // InventoryManager_Early.Instance.ReturnFromSWBST(currentGem);
        currentGem = null;
    }

    public Gem_Early GetCurrentGem()
    {
        return currentGem;
    }

    private void UpdatePlaceholderVisibility()
    {
        placeholderText.gameObject.SetActive(currentGem == null);
    }

    public bool compareGemTypeToSlotType()
    {
        if (currentGem == null)
        {
            isCorrect = false;
            return false;
        }

        bool matches = currentGem.Type.ToString().ToLower() == slotType.ToString().ToLower();
        isCorrect = matches;
        // UpdateResetButtonVisibility();
        return matches;
    }

    private void UpdateResetButtonVisibility()
    {
        if (resetButton != null)
        {
            // Hide reset button if gem is correct
            resetButton.gameObject.SetActive(!isCorrect);
        }
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }

    public void SetCorrectness(bool correct)
    {
        isCorrect = correct;
        UpdateResetButtonVisibility();
    }
}