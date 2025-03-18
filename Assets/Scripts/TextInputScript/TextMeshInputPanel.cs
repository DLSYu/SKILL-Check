using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TextMeshInputPanel : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI _tmp;
    public RectTransform rt;
    public delegate void OnClick(TextMeshProUGUI _tmp);
    public event OnClick onClick;
    public string POS;
    public InTextDefinition dictionaryDefinition;
    private int wordIndex;
    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
#if UNITY_ANDROID
        foreach (Touch touch in Input.touches)
        {
            // for the first finger on the screen
            if (touch.fingerId == 0)
            {
                if (IsInsidePanel(touch.position))
                {
                    TryInvokeClick();
                }
            }
        }
#else
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            if (IsInsidePanel(Input.mousePosition))
            {
                TryInvokeClick();
            }
        }
#endif


    }
    public void OnPointerDown(PointerEventData eventData)
    {
        TryInvokeClick();
    }

    public bool IsInsidePanel(Vector3 position)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        if ((position.x > bottomLeft.x && position.x < topRight.x) && (position.y > bottomLeft.y && position.y < topRight.y))
        {
            return true;
        }
        return false;
    }

    public void TryInvokeClick()
    {
        if (_tmp == null)
        {
            Debug.LogError("TextInputPanel has no _TMP reference!");
        }

        if (!dictionaryDefinition.exists)
        {
            Destroy(this.gameObject);
        }

        Debug.Log(dictionaryDefinition);
        onClick?.Invoke(_tmp);
    }

    public void SetWordIndex(int wordIndex)
    {
        this.wordIndex = wordIndex;
    }
}