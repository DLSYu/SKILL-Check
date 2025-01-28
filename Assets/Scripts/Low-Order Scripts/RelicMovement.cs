using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class RelicMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool dragging = false;

    [SerializeField] private float holdMinDuration = 1f;

    private Vector3 initLocalScale;
    private Transform originalParent;
    private Transform newParent;

    Collider2D inCollisionWith;

    // Start is called before the first frame update
    void Start()
    {
        initLocalScale = transform.localScale;
        originalParent = transform.parent;
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
                RelicSlot slot = newParent.GetComponent<RelicSlot>();

                if (slot.relic != null)
                {
                    Debug.Log(slot.relic.name);
                    slot.relic.GetComponent<RelicMovement>().OnSwap(originalParent);
                }

                transform.SetParent(newParent);
            }
            else
            {
                transform.SetParent(originalParent);
            }

            transform.localPosition = Vector3.zero;
            dragging = false;
        }
        else
        {
            //tapped
            //  - instantiate reading mechanic textbox
        }

        dragging = false;
        originalParent = transform.parent;
        newParent = null;
        transform.parent.GetComponent<RelicSlot>().relic = gameObject;
    }

    private void StartDragging()
    {
        StartCoroutine(SizeDown());
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        dragging = true;
        originalParent.GetComponent<RelicSlot>().relic = null;
    }

    public void OnSwap(Transform swapperParent)
    {
        transform.SetParent(swapperParent);
        originalParent = swapperParent;
        transform.localPosition = Vector3.zero;
        transform.parent.GetComponent<RelicSlot>().relic = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<RelicSlot>())
        {
            inCollisionWith = collision;
            newParent = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision == inCollisionWith)
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
