using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicCheckedSlot : RelicSlot
{
    [Header("Dragon Parts")]
    public GameObject originalPart;    // The visible dragon part (e.g. SplitHead)
    public GameObject hiddenPart;     // The replacement (e.g. Split-Head-Hidden)

    [Header("Validation")]
    public GameObject correctRelic;
    public bool IsCorrect { get; private set; }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{name} State - Original: {originalPart.activeSelf} Hidden: {hiddenPart.activeSelf}");
        }
    }

    void OnValidate()
    {
        UpdateSlotVisuals(placedRelic);
    }

    public void UpdateSlotVisuals(GameObject relic)
    {
        bool hasRelic = relic != null;
        IsCorrect = hasRelic && (relic == correctRelic);

        // Always update visuals based on relic presence
        if (originalPart != null) originalPart.SetActive(!hasRelic);
        if (hiddenPart != null) hiddenPart.SetActive(hasRelic);

        Debug.Log($"{name} visual update: {(hasRelic ? "has relic" : "empty")}");
    }

    public void ResetToOriginal()
    {
        // Only reset if slot is actually empty
        if (placedRelic == null)
        {
            if (originalPart != null) originalPart.SetActive(true);
            if (hiddenPart != null) hiddenPart.SetActive(false);
        }
        IsCorrect = false;
    }
}
