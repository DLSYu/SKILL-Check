using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool dragging = false;

    [SerializeField] private float holdMinDuration = 1f;

    private Vector3 initLocalScale;
    private Transform originalParent; // The original RelicSlot
    private Transform newParent; // The new RelicPlace or RelicSlot

    Collider2D inCollisionWith;

    // Start is called before the first frame update
    void Start()
    {
        initLocalScale = transform.localScale;
        originalParent = transform.parent; // Set the original parent (RelicSlot)
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke("StartDragging", holdMinDuration);
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

        if (dragging)
        {
            transform.localScale = initLocalScale;

            if (newParent != null)
            {
                // Check if the new parent is a RelicPlace
                RelicPlace newRelicPlace = newParent.GetComponent<RelicPlace>();
                if (newRelicPlace != null)
                {
                    newRelicPlace.PlaceRelic(gameObject);
                }

                transform.SetParent(newParent);
            }
            else
            {
                // If not dropped into a valid RelicPlace, return to the original RelicSlot
                transform.SetParent(originalParent);
            }

            transform.localPosition = Vector3.zero; // Reset position
            dragging = false;

            // Notify the old RelicPlace (if any) that the relic has been removed
            RelicPlace oldRelicPlace = originalParent.GetComponent<RelicPlace>();
            if (oldRelicPlace != null)
            {
                oldRelicPlace.RemoveRelic();
            }

            // Update the RelicSlot reference
            RelicSlot slot = transform.parent.GetComponent<RelicSlot>();
            if (slot != null)
            {
                slot.relic = gameObject;
            }

            // Check if the sorting is complete
            SortingGameManager.Instance.CheckCompletion();
        }
        else
        {
            //tapped
            //  - instantiate reading mechanic textbox
        }

        dragging = false;
        originalParent = transform.parent; // Update the original parent
        newParent = null;
    }

    private void StartDragging()
    {
        StartCoroutine(SizeDown());
        transform.SetParent(transform.root); // Move to the root of the hierarchy
        transform.SetAsLastSibling(); // Ensure it renders on top
        dragging = true;

        // Notify the old RelicPlace (if any) that the relic is being dragged
        RelicPlace oldRelicPlace = originalParent.GetComponent<RelicPlace>();
        if (oldRelicPlace != null)
        {
            oldRelicPlace.RemoveRelic();
        }

        // Clear the RelicSlot reference
        RelicSlot slot = originalParent.GetComponent<RelicSlot>();
        if (slot != null)
        {
            slot.relic = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<RelicPlace>() || collision.transform.GetComponent<RelicSlot>())
        {
            inCollisionWith = collision;
            newParent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == inCollisionWith)
        {
            newParent = null;
        }
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