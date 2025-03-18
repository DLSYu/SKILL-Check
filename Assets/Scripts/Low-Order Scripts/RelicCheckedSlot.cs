using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicCheckedSlot : RelicSlot
{
    public GameObject correctRelic;
    public Image partialImage;
    public Sprite defaultSprite;
    public Sprite partialSprite;
    private Image slotImage;

    public bool IsCorrect { get; private set; }

    void Start()
    {
        slotImage = GetComponent<Image>();
        ResetSlot();
    }

    public void UpdateSlotVisuals(GameObject relic)
    {
        bool hasRelic = relic != null;
        bool isCorrect = hasRelic && (relic == correctRelic);

        // Always show partial image when occupied
        partialImage.enabled = hasRelic;

        // Change color to indicate correctness without giving away answer
        partialImage.color = isCorrect ? Color.white : new Color(1, 1, 1, 0.4f);

        // Track correctness for game completion
        IsCorrect = isCorrect;
    }

    public void ResetSlot()
    {
        IsCorrect = false;
        partialImage.enabled = false;
        slotImage.sprite = defaultSprite;
    }
}
