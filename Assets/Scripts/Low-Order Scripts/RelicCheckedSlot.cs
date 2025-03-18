using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCheckedSlot : RelicSlot
{
    [Header("Dragon Parts")]
    public GameObject originalPart;    // The visible dragon part (e.g. SplitHead)
    public GameObject hiddenPart;     // The replacement (e.g. Split-Head-Hidden)

    [Header("Validation")]
    public GameObject correctRelic;
    public bool IsCorrect { get; private set; }

    public void UpdateSlotVisuals(GameObject relic)
    {
        bool hasRelic = relic != null;
        IsCorrect = hasRelic && (relic == correctRelic);

        // Only modify this slot's parts
        if (originalPart != null) originalPart.SetActive(!hasRelic);
        if (hiddenPart != null) hiddenPart.SetActive(hasRelic);
    }

    public void ResetSlot()
    {
        if (originalPart != null) originalPart.SetActive(true);
        if (hiddenPart != null) hiddenPart.SetActive(false);
        IsCorrect = false;
    }
}
