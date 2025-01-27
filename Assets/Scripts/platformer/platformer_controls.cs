using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class platformer_controls : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    [SerializeField]
    private GameObject joystick_template;
    private GameObject joystick_object;

    private float timeCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("test");
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log("Game Object Clicked!");
    }


     public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag: " + data.position);
        joystick_object = Instantiate(joystick_template, new Vector3(data.position.x, data.position.y, 0f), Quaternion.identity);
    }

    public void OnDrag(PointerEventData data)
    {
        if (data.dragging)
        {
            timeCount += Time.deltaTime;
            if (timeCount > 1.0f)
            {
                Debug.Log("Dragging:" + data.position);
                timeCount = 0.0f;
            }
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag: " + data.position);
        joystick_object.SetActive(false);
        Destroy(joystick_object);
    }
}
