using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Gem_Early), typeof(CanvasGroup))]
public class GemDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Only trigger if not dragging
        if (!eventData.dragging)
        {
            GetComponent<RelicPopupHandler>()?.OnRelicTapped();
        }
    }

    void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f; // Semi-transparent while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Check for SWBST slot drop
        SWBSTSlot slot = eventData.pointerCurrentRaycast.gameObject?.GetComponent<SWBSTSlot>();
        if (slot != null && slot.TryPlaceGem(GetComponent<Gem_Early>()))
        {
            // Successfully placed in slot
            Debug.Log($"Dropped on: {eventData.pointerCurrentRaycast.gameObject?.name}");
        }
        else
        {
            // Return to original position
            transform.position = originalPosition;
        }
    }
}