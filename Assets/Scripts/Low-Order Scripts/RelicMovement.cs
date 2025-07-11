using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class RelicMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] public bool useWorldSpace = false;
    public bool isMovable = true;
    [SerializeField] private bool isQuickSort = false;

    // FOR PIVOT SCENE LOGIC { private get; set; }
    [SerializeField] public bool isPivotScene = false;
    [SerializeField] private GameObject beforeOrAfter;
    [SerializeField] public GameObject pivotSceneManager;

    private bool dragging = false;
    private bool isAttemptingDrag = false; // New flag to track drag attempt

    [SerializeField] private float holdMinDuration = 0.5f;

    private Vector3 initLocalScale;
    // The original RelicSlot
    [SerializeField] private RelicSlot _originalParent;
    public RelicSlot originalParent
    {
        get => _originalParent;
        set => _originalParent = value;
    }

    [SerializeField] private RelicSlot newParent; // The new RelicPlace or RelicSlot

    public RelicPopupHandler popupHandler; // Reference to RelicPopupHandler
    private StorySegment storySegment;

    private Coroutine sizeDownCoroutine;
    [SerializeField] private bool randomizedAtStart = true;

    // Method for RelicSlot to check if the relic is being dragged
    public bool IsDragging()
    {
        return dragging;
    }

    Collider2D inCollisionWith;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log($"IN START() OF RELICMOVEMENT OF {name}");
        storySegment = GetComponent<StorySegment>();

        initLocalScale = transform.localScale;
        if (transform.parent.GetComponent<RelicSlot>() != null)
        {
            transform.parent.GetComponent<RelicSlot>().PlaceRelic(gameObject);
            originalParent = transform.parent.GetComponent<RelicSlot>(); // Set the original parent (RelicSlot)
            Debug.Log($"set original at movement: {name}");
        }
        Debug.Log($"EXITING START() OF RELICMOVEMENT OF {name}");
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
                Vector3 point = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
                transform.position = new Vector3(point.x, point.y, transform.position.z);
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
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
        if (!sizeDownCoroutine.IsUnityNull())
        {
            StopCoroutine(sizeDownCoroutine);
            sizeDownCoroutine = null;
        }
        transform.localScale = initLocalScale;

        // Handle tap if we released before hold duration
        //if (isAttemptingDrag && popupHandler != null)
        //{
        //    popupHandler.OnRelicTapped();
        //}

        if ((isAttemptingDrag || !isMovable) && storySegment != null)
        {
            storySegment.ReadStorySegment();
        }

        isAttemptingDrag = false;

        if (dragging)
        {

            //transform.localScale = initLocalScale;
            RelicSlot previousParent = originalParent;

            if (isPivotScene)
            {
                if (beforeOrAfter.IsUnityNull())
                {
                    previousParent.placedRelic = gameObject;
                    transform.SetParent(previousParent.transform);
                    transform.localPosition = Vector3.zero;
                } else
                {
                    if (beforeOrAfter.name == "BeforeButton")
                    {
                        pivotSceneManager.GetComponent<PivotSceneManager>().BeforeButtonClick();
                    } else if (beforeOrAfter.name == "AfterButton")
                    {
                        pivotSceneManager.GetComponent<PivotSceneManager>().AfterButtonClick();
                    }
                }

                dragging = false;
                return;
            }

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
            if (!isQuickSort)
            {
                SortingGameManager.Instance.CheckCompletion();
            }
            else
            {
                SortingGameManager.Instance.QuickSortCheckCompletion();
            }

            newParent = null;
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

        sizeDownCoroutine = StartCoroutine(SizeDown());
        if(!isPivotScene) transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        dragging = true;
        isAttemptingDrag = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPivotScene)
        {
            if (collision.name == "BeforeButton" || collision.name == "AfterButton")
            {
                beforeOrAfter = collision.gameObject;
            }

            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPivotScene)
        {
            if(collision.name == "BeforeButton" || collision.name == "AfterButton")
            {
                beforeOrAfter = collision.gameObject;
            }

            return;
        }

        RelicSlot slot = collision.GetComponent<RelicSlot>();
        if (slot != null && slot != originalParent && newParent != originalParent)
        {
            newParent = slot;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPivotScene)
        {
            if (collision.name == "BeforeButton" || collision.name == "AfterButton")
            {
                beforeOrAfter = null;
            }

            return;
        }

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