using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TextMeshInputPanel : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI _tmp;
    public string text;
    public RectTransform rt;
    public delegate void OnClick(TextMeshProUGUI _tmp);
    public event OnClick onClick;
    public string POS;
    public InTextDefinition dictionaryDefinition;
    public int wordIndex;
    public DictionaryManager dictionaryManager;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        dictionaryManager = FindObjectOfType<DictionaryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
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

        dictionaryDefinition = DictionaryReader.ReadDictionaryFirstValid(text.ToLower());

        // Get dictionary if missing
        if (dictionaryDefinition.definition == "" || dictionaryDefinition.example == "")
        {
            // get the other POS that has definition and example sentence
            dictionaryDefinition = DictionaryReader.ReadDictionaryFirstValid(text.ToLower());
            Debug.Log("Got Diff Definition");
        }

        if (dictionaryDefinition == null)
        {
            Destroy(this.gameObject);
        }

        Debug.Log(dictionaryDefinition + "end");

        dictionaryManager.ShowDictionaryPanel(text, dictionaryDefinition);
        onClick?.Invoke(_tmp);
    }

    public void SetWordIndex(int wordIndex)
    {
        this.wordIndex = wordIndex;
    }

    public int GetWordIndex()
    {
        return wordIndex;
    }
}