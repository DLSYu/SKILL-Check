using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class DraggableRelic : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] 
    private Canvas canvas;
    private float x;
    private float y;

    private Vector3 original_scale;
    private Vector2 offset;

    private void Awake() {

    }

    public void OnBeginDrag(PointerEventData eventData){
        this.GetComponent<CanvasGroup>().alpha = .6f;
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        

    }

    public void OnDrag(PointerEventData eventData){
        
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
        

    }

    public void OnEndDrag(PointerEventData eventData){
        this.GetComponent<CanvasGroup>().alpha = 1f;
       this.GetComponent<RectTransform>().position = new Vector3(x, y);
       this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
         RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out offset
        );

        // Center the offset (makes sure object is centered on the mouse)
        offset = rectTransform.anchoredPosition - offset;
    }
    void Start()
    {
        x = this.GetComponent<RectTransform>().position.x;
        y = this.GetComponent<RectTransform>().position.y;
        original_scale = this.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
