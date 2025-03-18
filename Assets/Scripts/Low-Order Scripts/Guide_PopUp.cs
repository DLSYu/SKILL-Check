using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBubblePopup : MonoBehaviour
{
    public Image textBubbleImage; // Reference to the text bubble
    public TextMeshProUGUI textInsideBubble; // Reference to the text inside the bubble
    public float fadeDuration = 1f; // Duration of the fade effect

    private CanvasGroup canvasGroup;

    private void Start()
    {
        // Ensure the GameObject is active
        if (!gameObject.activeSelf)
        {
            Debug.LogWarning($"{gameObject.name} was inactive at Start. Activating it.");
            gameObject.SetActive(true);
        }

        // Add a CanvasGroup if not already present
        if (!textBubbleImage.gameObject.TryGetComponent(out canvasGroup))
        {
            canvasGroup = textBubbleImage.gameObject.AddComponent<CanvasGroup>();
        }

        // Start the pop-up coroutine
        StartCoroutine(ShowTextBubble());
    }

    private IEnumerator ShowTextBubble()
    {
        // Show the text bubble and text
        textBubbleImage.gameObject.SetActive(true);
        textInsideBubble.gameObject.SetActive(true);

        // Fade in
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 0f, 1f, fadeDuration));

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Fade out
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, 1f, 0f, fadeDuration));

        // Hide the text bubble and text
        textBubbleImage.gameObject.SetActive(false);
        textInsideBubble.gameObject.SetActive(false);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup group, float startAlpha, float endAlpha, float duration)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            group.alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        group.alpha = endAlpha; // Ensure the final alpha is set
    }
}
