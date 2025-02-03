using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;


public class SwipableText : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    
    [SerializeField]
    private GameObject karaoke_line;

    private TMP_Text m_TextComponent;
    private TextMeshProUGUI m_TextMeshProUGUI;

    private Vector3 targetPosition;
    private bool allowDragging;
    private int previousLineNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        m_TextComponent = this.GetComponent<TMP_Text>();
        m_TextMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
       // m_Transform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (karaoke_line != null)
        {
            RectTransform rt = karaoke_line.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector3.Lerp(rt.anchoredPosition, targetPosition, Time.deltaTime * 15f); // Adjust speed here
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("Game Object Clicked!");
         allowDragging = true;
        
       
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("Game Object Pointer Up");
         allowDragging = true;
        
       
    }


     public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag: " + data.position);
       
    }


private int FindNearestCharacterInLine(Vector2 screenPoint, int lineNumber, Camera uiCamera)
{
    if (lineNumber < 0 || lineNumber >= m_TextMeshProUGUI.textInfo.lineCount)
    {
        Debug.LogWarning("Invalid line number!");
        return -1;
    }

    TMP_LineInfo lineInfo = m_TextMeshProUGUI.textInfo.lineInfo[lineNumber];

    int firstCharIndex = lineInfo.firstCharacterIndex;
    int lastCharIndex = firstCharIndex + lineInfo.characterCount - 1;

    float closestDistance = float.MaxValue;
    int nearestIndex = firstCharIndex;

    for (int i = firstCharIndex; i <= lastCharIndex; i++)
    {
        TMP_CharacterInfo charInfo = m_TextMeshProUGUI.textInfo.characterInfo[i];

        // Convert world character position to screen space
        Vector3 charScreenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, m_TextMeshProUGUI.transform.TransformPoint(charInfo.bottomLeft));

        float distance = Vector2.Distance(screenPoint, charScreenPos);
        
        if (distance < closestDistance)
        {
            closestDistance = distance;
            nearestIndex = i;
        }
    }

    Debug.Log($"Nearest character in line {lineNumber}: '{m_TextMeshProUGUI.textInfo.characterInfo[nearestIndex].character}' at index {nearestIndex}");
    return nearestIndex;
}

    public void OnDrag(PointerEventData data)
    {
        bool setPos = false;
        
        if (data.dragging && allowDragging)
        {
            float offset = 0;
            int index = TMP_TextUtilities.FindNearestCharacterOnLine(m_TextComponent, data.position, previousLineNumber, data.enterEventCamera, false);
            
            TMP_CharacterInfo characterInfo = m_TextMeshProUGUI.textInfo.characterInfo[index];

         
            if (previousLineNumber - 1 >= 0)
            {
                if (index == m_TextComponent.textInfo.lineInfo[previousLineNumber].firstCharacterIndex && 
                    TMP_TextUtilities.FindNearestLine(m_TextComponent, data.position, data.enterEventCamera) < previousLineNumber) // outside range
                {
                    int lastCharacterIndex = m_TextComponent.textInfo.lineInfo[previousLineNumber-1].lastCharacterIndex;
                    characterInfo = m_TextMeshProUGUI.textInfo.characterInfo[lastCharacterIndex];
                    previousLineNumber -= 1;

                    allowDragging = false;
                    setPos = true;
                }
            }
            
            // check if index is at the last character for that line number (go to next line)
            if (previousLineNumber + 1 < m_TextComponent.textInfo.lineCount && !setPos)
            {
                Debug.Log("val1: " + index  + " / " + m_TextComponent.textInfo.lineInfo[previousLineNumber].lastCharacterIndex);
                Debug.Log("val2: " + TMP_TextUtilities.FindNearestLine(m_TextComponent, data.position, data.enterEventCamera));
                if (index == m_TextComponent.textInfo.lineInfo[previousLineNumber].lastCharacterIndex-1 && 
                    (TMP_TextUtilities.FindNearestLine(m_TextComponent, data.position, data.enterEventCamera) > previousLineNumber)) // outside range
                {
                    int firstCharacterIndex = m_TextComponent.textInfo.lineInfo[previousLineNumber+1].firstCharacterIndex;
                    characterInfo = m_TextMeshProUGUI.textInfo.characterInfo[firstCharacterIndex];
                    previousLineNumber += 1;
                    
                    allowDragging = false;
                    setPos = true;

                }
                
            }
                // if not, find nearest character given line number
            if (!setPos) 
            {
                int nearestIndex = FindNearestCharacterInLine(data.position, previousLineNumber, data.pressEventCamera);
                characterInfo = m_TextMeshProUGUI.textInfo.characterInfo[nearestIndex];
            }

            
            // last character
            if (index == m_TextComponent.textInfo.characterCount - 2 && TMP_TextUtilities.FindIntersectingCharacter(m_TextComponent, data.position, data.enterEventCamera, false) == -1)
                offset = 20f;

            Debug.Log("index: " + index + " / " + m_TextComponent.textInfo.characterCount);
            // Calculate target position
            targetPosition = new Vector3(characterInfo.bottomLeft.x + offset, (characterInfo.ascender + characterInfo.descender) / 2, 0);
            
            
        }
    }

    

    public void OnEndDrag(PointerEventData data)
    {

        Debug.Log("OnEndDrag: " + data.position);
    
        

    }

}
