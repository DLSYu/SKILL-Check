using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

[RequireComponent(typeof(RectTransform))]
public class TextMeshInputPanel : MonoBehaviour, IPointerClickHandler
{
    public RectTransform rt;
    public delegate void OnClick(TextMeshProUGUI _tmp);
    public event OnClick onClick;
    private int wordIndex;
    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TextMeshProUGUI _tmp = GetComponentInParent<TextMeshProUGUI>();
        if (_tmp == null)
        {
            Debug.LogError("TextInputPanel must be the child to a TextMeshInputHandler object!");
        }
        Debug.Log(_tmp.textInfo.wordInfo[wordIndex].GetWord());
        onClick?.Invoke(_tmp);
    }
    public void SetWordIndex(int wordIndex)
    {
        this.wordIndex = wordIndex;
    }
}