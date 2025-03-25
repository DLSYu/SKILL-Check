using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class RelicMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private bool useWorldSpace = false;
    public bool isMovable = true;
    private bool dragging = false;
    private bool isAttemptingDrag = false; // New flag to track drag attempt

    [SerializeField] private float holdMinDuration = 0.5f;

    private Vector3 initLocalScale;
    // The original RelicSlot
    private RelicSlot _originalParent;
    public RelicSlot originalParent
    {
        get => _originalParent;
        set => _originalParent = value;
    }

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
        if (transform.parent.GetComponent<RelicSlot>() != null)
        {
            originalParent = transform.parent.GetComponent<RelicSlot>(); // Set the original parent (RelicSlot)
        }
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
            if (useWorldSpace)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                transform.position = ray.GetPoint(1000);
            }
            else
            {
                transform.position = Input.mousePosition;
            }

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
            RelicSlot previousParent = originalParent;

            if (newParent != null)
            {
                newParent.PlaceRelic(gameObject);
            }
            else
            {
                previousParent.placedRelic = gameObject;
                transform.SetParent(previousParent.transform);
                transform.localPosition = Vector3.zero;
            }

            // Explicitly update previous parent visuals
            if (previousParent != null)
            {
                previousParent.UpdateCheckedSlotVisuals(previousParent);
            }

            originalParent = newParent ?? originalParent;
            SortingGameManager.Instance.CheckCompletion();
            dragging = false;
        }
    }

    private void StartDragging()
    {
        if (!isAttemptingDrag) return;

        // If coming from a checked slot, reset its visuals immediately
        if (originalParent != null)
        {
            RelicCheckedSlot previousSlot = originalParent.GetComponent<RelicCheckedSlot>();
            if (previousSlot != null)
            {
                previousSlot.ResetToOriginal();
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
        RelicSlot slot = collision.GetComponent<RelicSlot>();
        if (slot != null && slot != originalParent)
        {
            newParent = slot;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RelicSlot>() == newParent)
        {
            newParent = null;
        }
    }

    public void OnSwap(RelicSlot destinationRelicSlot)
    {
        _originalParent = destinationRelicSlot;
        transform.SetParent(destinationRelicSlot.transform);
        transform.localPosition = Vector3.zero;
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