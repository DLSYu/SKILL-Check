using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBubblePopup : MonoBehaviour
{
    public Image textBubbleImage; // Reference to the text bubble
    public TextMeshProUGUI textInsideBubble; // Reference to the text inside the bubble

    private void Start()
    {
        // Ensure the GameObject is active
        if (!gameObject.activeSelf)
        {
            Debug.LogWarning($"{gameObject.name} was inactive at Start. Activating it.");
            gameObject.SetActive(true);
        }

        // Ensure the bubble is hidden at the start
        textBubbleImage.gameObject.SetActive(false);
        textInsideBubble.gameObject.SetActive(false);

        // Start the pop-up coroutine
        StartCoroutine(ShowTextBubble());
    }

    private IEnumerator ShowTextBubble()
    {
        // Show the text bubble and text
        textBubbleImage.gameObject.SetActive(true);
        textInsideBubble.gameObject.SetActive(true);

        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Hide the text bubble and text
        textBubbleImage.gameObject.SetActive(false);
        textInsideBubble.gameObject.SetActive(false);
    }
}
