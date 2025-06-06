using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicPopupHandler : MonoBehaviour
{
    public GameObject relicPopupPanel; // Reference to the pop-up panel
    public TextMeshProUGUI relicText; // Reference to the text component
    public string passage; // The passage to display
    private bool shouldHighlight = false; // Track highlight state

    private bool isPopupActive = false;
    [SerializeField] private GemInterface gem; // Reference to the gem object

    [SerializeField] private Color highlightColor = Color.red;
    private string originalPassage;

    void Start()
    {
        // Store original passage
        if (gem != null)
        {
            originalPassage = gem.GemDescription;
            relicText.text = originalPassage;
        }

        // Ensure the pop-up is hidden at the start
        if (relicPopupPanel != null)
        {
            relicPopupPanel.SetActive(false);
        }
    }

    public void SetShouldHighlight(bool highlight)
    {
        shouldHighlight = highlight;
        Debug.Log($"SetShouldHighlight called with: {highlight} for gem: {gem?.GemName}");
        Debug.Log($"Gem keyword: {gem?.Keyword}");
        Debug.Log($"Original passage length: {originalPassage?.Length}");

        // Apply highlighting immediately when the flag is set
        UpdateTextDisplay();
    }

    private void UpdateTextDisplay()
    {
        if (gem == null || string.IsNullOrEmpty(originalPassage))
        {
            Debug.LogWarning("Gem or original passage is null");
            return;
        }

        if (shouldHighlight && !string.IsNullOrEmpty(gem.Keyword))
        {
            ApplyHighlight();
        }
        else
        {
            ResetHighlight();
        }
    }

    // Call this method when the relic is tapped
    public void OnRelicTapped()
    {
        if (!isPopupActive)
        {
            relicPopupPanel.SetActive(true);
            isPopupActive = true;

            // Update text display based on current highlight state
            UpdateTextDisplay();
        }
    }

    private void ApplyHighlight()
    {
        if (gem == null || string.IsNullOrEmpty(gem.Keyword))
        {
            Debug.LogWarning("Cannot apply highlight - gem or keyword is null");
            return;
        }

        string keyword = gem.Keyword;
        Debug.Log($"Applying highlight for keyword: {keyword}");

        if (originalPassage.Contains(keyword))
        {
            string highlightedPassage = originalPassage.Replace(
                keyword,
                $"<color=#{ColorUtility.ToHtmlStringRGBA(highlightColor)}><b>{keyword}</b></color>"
            );
            relicText.text = highlightedPassage;
            Debug.Log("Keyword highlighted successfully");
        }
        else
        {
            Debug.LogWarning($"Keyword '{keyword}' not found in passage: {originalPassage}");
            relicText.text = originalPassage;
        }
    }

    public void OnCloseButtonClicked()
    {
        if (isPopupActive)
        {
            // Hide the pop-up
            relicPopupPanel.SetActive(false);
            isPopupActive = false;
        }
    }

    public void ResetHighlight()
    {
        if (gem != null && !string.IsNullOrEmpty(originalPassage))
        {
            relicText.text = originalPassage;
            Debug.Log($"Reset highlight for gem: {gem.GemName}");
        }
    }

    void Update()
    {
        // Temporary debug control - remove after testing
        if (Input.GetKeyDown(KeyCode.H))
        {
            shouldHighlight = !shouldHighlight;
            Debug.Log($"Toggled highlight: {shouldHighlight}");
            if (isPopupActive)
            {
                if (shouldHighlight) ApplyHighlight();
                else relicText.text = originalPassage;
            }
        }
    }
}
