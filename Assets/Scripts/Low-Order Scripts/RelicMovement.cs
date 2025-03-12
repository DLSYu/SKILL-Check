using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool isMovable = true;
    private bool dragging = false;

    [SerializeField] private float holdMinDuration = 0.5f;

    private Vector3 initLocalScale;
    public RelicSlot originalParent { get; private set; } // The original RelicSlot
    [SerializeField] private RelicSlot newParent; // The new RelicPlace or RelicSlot

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
        if (isMovable) Invoke("StartDragging", holdMinDuration);
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

            //Check if the relic was dropped into a RelicSlot
            if (newParent != null)
            {
                // Assign new RelicSlot into newParent
                RelicSlot newRelicSlot = newParent.GetComponent<RelicSlot>();

                //Place RelicPart into newRelicSlot and check if swapped
                //(true = swapped; false = not swapped)
                if (newRelicSlot.PlaceRelic(gameObject))
                {
                    //Remove RelicPart of the previous RelicSlot
                    originalParent.RemoveRelic();
                }

                //Assign new transfor.parent
                transform.SetParent(newParent.transform);

            }
            else
            {
                // If not dropped into a valid RelicSlot, return to the original RelicSlot
                transform.SetParent(originalParent.transform);
            }

            transform.localPosition = Vector3.zero; // Reset position
            dragging = false;

            // Check if the sorting is complete
            SortingGameManager.Instance.CheckCompletion();
        }
        else
        {
            //tapped
            //  - instantiate reading mechanic textbox
        }

        originalParent = transform.parent.GetComponent<RelicSlot>(); // Update the original parent
        newParent = null;
    }

    private void StartDragging()
    {
        StartCoroutine(SizeDown());
        transform.SetParent(transform.root); // Move to the root of the hierarchy
        transform.SetAsLastSibling(); // Ensure it renders on top
        dragging = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<RelicSlot>())
        {
            inCollisionWith = collision;
            newParent = collision.transform.GetComponent<RelicSlot>();
            //Debug.Log($"Staying at {collision.name}");
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