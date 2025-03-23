using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
 *  Relic Slot is a GameObject that holds a Relic
 */
public class RelicSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject placedRelic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when a RelicPart is placed here
    // return true if a swap happened and false if no placedRelic was found and no swap happened.s
    public bool PlaceRelic(GameObject relicToPlace)
    {
        if (placedRelic == relicToPlace)
            return false;

        RelicMovement relicMovement = relicToPlace.GetComponent<RelicMovement>();
        RelicSlot fromSlot = relicMovement?.originalParent;

        if (placedRelic != null)
        {
            if (fromSlot != null)
            {
                fromSlot.SwapRelics(this);
            }
            return true;
        }

        // New placement
        placedRelic = relicToPlace;
        relicToPlace.transform.SetParent(transform);
        relicToPlace.transform.localPosition = Vector3.zero;

        // Clear original parent's relic reference
        if (fromSlot != null)
        {
            fromSlot.placedRelic = null;
            fromSlot.RemoveRelic(); // Triggers visual reset
        }

        UpdateCheckedSlotVisuals(this);
        return true;
    }

    // Called when a RelicPart is removed
    public void RemoveRelic()
    {
        // Clear the relic FIRST
        GameObject removedRelic = placedRelic;
        placedRelic = null;

        // Handle checked slot reset AFTER clearing
        RelicCheckedSlot checkedSlot = GetComponent<RelicCheckedSlot>();
        if (checkedSlot != null)
        {
            checkedSlot.ResetToOriginal();
        }
    }

    public void SwapRelics (RelicSlot swapWith)
    {
        GameObject currentRelic = placedRelic;
        GameObject incomingRelic = swapWith.placedRelic;

        // --- STEP 1: Reset visuals on the original checked slots (if applicable) ---
        if (currentRelic != null)
        {
            RelicMovement currentMovement = currentRelic.GetComponent<RelicMovement>();
            if (currentMovement != null && currentMovement.originalParent != null)
            {
                RelicCheckedSlot cs = currentMovement.originalParent.GetComponent<RelicCheckedSlot>();
                if (cs != null)
                {
                    cs.ResetToOriginal();
                }
            }
        }
        if (incomingRelic != null)
        {
            RelicMovement incomingMovement = incomingRelic.GetComponent<RelicMovement>();
            if (incomingMovement != null && incomingMovement.originalParent != null)
            {
                RelicCheckedSlot cs = incomingMovement.originalParent.GetComponent<RelicCheckedSlot>();
                if (cs != null)
                {
                    cs.ResetToOriginal();
                }
            }
        }

        // --- STEP 2: Clear both slots immediately ---
        this.placedRelic = null;
        swapWith.placedRelic = null;

        // --- STEP 3: Reparent relics (optionally animate the movement for smoothness) ---
        if (currentRelic != null)
        {
            currentRelic.transform.SetParent(swapWith.transform);
            // Replace the next line with a tween if desired, e.g., using DOTween:
            // currentRelic.transform.DOLocalMove(Vector3.zero, 0.3f);
            currentRelic.transform.localPosition = Vector3.zero;
            swapWith.placedRelic = currentRelic;
            currentRelic.GetComponent<RelicMovement>().originalParent = swapWith;
        }

        if (incomingRelic != null)
        {
            incomingRelic.transform.SetParent(this.transform);
            incomingRelic.transform.localPosition = Vector3.zero;
            this.placedRelic = incomingRelic;
            incomingRelic.GetComponent<RelicMovement>().originalParent = this;
        }

        // --- STEP 4: Final visual update on both slots ---
        UpdateCheckedSlotVisuals(this);
        UpdateCheckedSlotVisuals(swapWith);
    }

    public void UpdateCheckedSlotVisuals(RelicSlot slot)
    {
        RelicCheckedSlot checkedSlot = slot.GetComponent<RelicCheckedSlot>();
        if (checkedSlot != null)
        {
            // Update visuals even if no relic is present.
            checkedSlot.UpdateSlotVisuals(slot.placedRelic);
        }
    }

    // Detect tap/click on the RelicSlot
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the tap/click is valid (e.g., not during dragging)
        if (!IsDraggingRelic())
        {
            Debug.Log($"Tapped on RelicSlot: {name}");
        }
    }

    // Check if a relic is currently being dragged
    private bool IsDraggingRelic()
    {
        // Check if any relic is being dragged
        RelicMovement[] relics = FindObjectsOfType<RelicMovement>();
        foreach (var relic in relics)
        {
            if (relic.IsDragging())
            {
                return true;
            }
        }
        return false;
    }
}
