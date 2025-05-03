using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicCheckedSlot : RelicSlot
{
    [Header("Validation")]
    public GameObject correctRelic;
    public bool IsCorrect { get; private set; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"{name} State - IsCorrect: {IsCorrect}");
        }
    }

    void OnValidate()
    {
        UpdateSlotVisuals(placedRelic);
    }

    public void UpdateSlotVisuals(GameObject relic)
    {
        IsCorrect = relic != null && (relic == correctRelic);
        Debug.Log($"{name} visual update: {(IsCorrect ? "has correct relic" : "empty or wrong")}");
    }

    public void ResetToOriginal()
    {
        if (placedRelic == null)
        {
            IsCorrect = false;
        }
    }
}