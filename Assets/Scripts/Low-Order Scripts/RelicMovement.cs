using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool isMovable = true;
    private bool dragging = false;
    private bool isAttemptingDrag = false; // New flag to track drag attempt

    [SerializeField] private float holdMinDuration = 0.5f;

    private Vector3 initLocalScale;
    public RelicSlot originalParent { get; private set; } // The original RelicSlot
    [SerializeField] private RelicSlot newParent; // The new RelicPlace or RelicSlot

    public RelicPopupHandler popupHandler; // Reference to RelicPopupHandler

    // Method for RelicSlot to check if the relic is being dragged
    public bool IsDragging()
    {
        return dragging;
    }

    Collider2D inCollisionWith;

    // Start is called before the first frame update
    void Start()
    {
        initLocalScale = transform.localScale;
        originalParent = transform.parent.GetComponent<RelicSlot>(); // Set the original parent (RelicSlot)
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isMovable)
        {
            isAttemptingDrag = true;
            Invoke("StartDragging", holdMinDuration);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragging)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("StartDragging");

        // Handle tap if we released before hold duration
        if (isAttemptingDrag && popupHandler != null)
        {
            popupHandler.OnRelicTapped();
        }
        
        isAttemptingDrag = false;

        if (dragging)
        {
            transform.localScale = initLocalScale;

            // Store previous parent
            RelicSlot previousParent = originalParent;

            if (newParent != null)
            {
                // Move to new parent
                transform.SetParent(newParent.transform);
                transform.SetAsLastSibling(); // Add this line
                transform.localPosition = Vector3.zero;

                // Update new slot
                RelicCheckedSlot newCheckedSlot = newParent.GetComponent<RelicCheckedSlot>();
                if (newCheckedSlot != null)
                {
                    newCheckedSlot.UpdateSlotVisuals(gameObject);
                }
            }
            else
            {
                // Return to original parent if exists
                if (originalParent != null && originalParent.transform != null)
                {
                    transform.SetParent(originalParent.transform);
                    transform.localPosition = Vector3.zero;
                }
            }

            // Reset previous slot
            if (previousParent != null)
            {
                RelicCheckedSlot previousCheckedSlot = previousParent.GetComponent<RelicCheckedSlot>();
                if (previousCheckedSlot != null)
                {
                    previousCheckedSlot.UpdateSlotVisuals(null); // Pass null to reset
                }
            }

            // Update original parent reference
            originalParent = (newParent != null) ? newParent.GetComponent<RelicSlot>() : previousParent;

            // Update game state
            SortingGameManager.Instance.CheckCompletion();
            dragging = false;
        }
    }

    private void StartDragging()
    {
        if (!isAttemptingDrag) return;

        // Reset previous slot if coming from a checked slot
        if (originalParent != null)
        {
            RelicCheckedSlot previousSlot = originalParent.GetComponent<RelicCheckedSlot>();
            if (previousSlot != null)
            {
                previousSlot.ResetSlot();
            }
        }

        StartCoroutine(SizeDown());
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        dragging = true;
        isAttemptingDrag = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RelicCheckedSlot dragonSlot = collision.GetComponent<RelicCheckedSlot>();

        if (collision.transform.GetComponent<RelicSlot>())
        {
            inCollisionWith = collision;
            newParent = collision.transform.GetComponent<RelicSlot>();
            //Debug.Log($"Staying at {collision.name}");
        }
        else if (dragonSlot != null)
        {
            inCollisionWith = collision;
            newParent = dragonSlot;
        }
        else
        {
            RelicSlot relicSlot = collision.GetComponent<RelicSlot>();
            if (relicSlot != null)
            {
                inCollisionWith = collision;
                newParent = relicSlot;
            }
            else
            {
                newParent = null;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == inCollisionWith)
        {
            newParent = null;
            //Debug.Log($"Exited {collision.name}");
        }
    }

    public RelicSlot OnSwap(RelicSlot destinationRelicSlot)
    {
        RelicSlot toReturn = originalParent;

        transform.SetParent(destinationRelicSlot.transform);
        originalParent = destinationRelicSlot;
        transform.localPosition = Vector3.zero;
        return toReturn;
    }

    IEnumerator SizeDown()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.localScale = new Vector3(
                    transform.localScale.x - 0.05f,
                    transform.localScale.y - 0.05f,
                    transform.localScale.z - 0.05f);
        }
    }
}