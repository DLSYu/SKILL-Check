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

    private bool isPopupActive = false;

    void Start()
    {
        // Ensure the pop-up is hidden at the start
        if (relicPopupPanel != null)
        {
            relicPopupPanel.SetActive(false);
        }
    }

    // Call this method when the relic is tapped
    public void OnRelicTapped()
    {
        if (!isPopupActive)
        {
            // Show the pop-up and set the passage text
            relicPopupPanel.SetActive(true);
            relicText.text = passage;
            isPopupActive = true;
        }
    }

    // Call this method when the close button is clicked
    public void OnCloseButtonClicked()
    {
        if (isPopupActive)
        {
            // Hide the pop-up
            relicPopupPanel.SetActive(false);
            isPopupActive = false;
        }
    }
}
