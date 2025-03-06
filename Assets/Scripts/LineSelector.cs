using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; // Required when using event data
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System;
using Unity.VisualScripting;
using System.Numerics;
using UnityEditor;
using UnityEngine.Animations;
using System.Collections.Generic;

public class LineSelector : MonoBehaviour, IEndDragHandler 
{
    private Scrollbar scrollbar;
    [SerializeField]
    private TextMeshProUGUI storyText;

    public List<int> nearestIndexes;

    void Awake()
    {
        scrollbar = this.gameObject.GetComponent<Scrollbar>();
        nearestIndexes.Add(0);
        
        
    }
    public void OnEndDrag(PointerEventData data)
    {

        
       
        UpdateNearestIndexes(data.position, data.enterEventCamera);
        UpdateScrollbarValue();

       
        

        
    }

void UpdateScrollbarValue(){
    TMP_TextInfo storyTextInfo = storyText.textInfo;
    TMP_CharacterInfo[] characterInfo = storyTextInfo.characterInfo;


    RectTransform rectTransform = storyText.rectTransform;
        // Get world corners of the RectTransform
    UnityEngine.Vector3[] worldCorners = new UnityEngine.Vector3[4];
    rectTransform.GetWorldCorners(worldCorners);
    
    UnityEngine.Vector3 worldBottomLeft = worldCorners[0];  // Bottom-left corner (world space)
        UnityEngine.Vector3 worldTopLeft = worldCorners[1];     // Top-left corner (world space)

        // Convert character positions to world space
        UnityEngine.Vector3 worldNearestTopLeft = rectTransform.TransformPoint(
            characterInfo[storyTextInfo.lineInfo[nearestIndexes[nearestIndexes.Count - 1]].firstCharacterIndex].topLeft
        );

        UnityEngine.Vector3 worldNearestBottomLeft = rectTransform.TransformPoint(
            characterInfo[storyTextInfo.lineInfo[nearestIndexes[0]].firstCharacterIndex].bottomLeft
        );

          // Calculate midpoint in world space
        float midpointY = (worldNearestTopLeft.y + worldNearestBottomLeft.y) / 2f;

        scrollbar.value = Mathf.InverseLerp(
            worldTopLeft.y,
            worldBottomLeft.y,
            midpointY
        );
}

public void ResetSliderToFirstLine()
{
        storyText.ForceMeshUpdate();

        nearestIndexes.Clear();
        SetSliderToNthSentence(1);

}

public int SetSliderToNthSentence(int n)
{
        bool start = false;
        int currentSentence = 0;
        List<int> tempIndex = new List<int>();
        storyText.ForceMeshUpdate();

        int currentTextLineCount = storyText.textInfo.lineCount;
        int firstCharIndex = storyText.textInfo.pageInfo[storyText.pageToDisplay-1].firstCharacterIndex;
        int lastCharIndex = storyText.textInfo.pageInfo[storyText.pageToDisplay-1].lastCharacterIndex;

        if (lastCharIndex == 0)
            lastCharIndex = storyText.textInfo.characterCount;


        Debug.Log($"Page {storyText.pageToDisplay}: First Char = {firstCharIndex}, Last Char = {lastCharIndex}");



        if (n >= 0)
        {

            for (int i = 0; i < currentTextLineCount; i++)
            {
                TMP_LineInfo lineInfo = storyText.textInfo.lineInfo[i];

                string s = storyText.text.Substring(lineInfo.firstCharacterIndex, lineInfo.characterCount).Trim((char)8203).Trim();


                // Ensure the line's first character is within the visible page range
                // start of a sentence and within a page
                if (lineInfo.firstCharacterIndex >= firstCharIndex && !start && lineInfo.lastCharacterIndex <= lastCharIndex && s.Length != 0)
                {
                    start = true;
                    currentSentence++;
                }

                if (lineInfo.lastCharacterIndex > lastCharIndex || currentSentence > n)
                    break;

                else if (start && s.Length == 0)
                {
                    
                    start = false;
                }
        
                if (currentSentence == n && s.Length != 0 && start)
                {
                    tempIndex.Add(i);
                }

                

            }
        }
      

        if (tempIndex.Count != 0)
        {
            nearestIndexes.Clear();
            nearestIndexes = tempIndex;

            UpdateScrollbarValue();
            return 0;
        }
        else 
            return -1;
            
        
}

private void UpdateNearestIndexes(UnityEngine.Vector2 screenPoint, Camera uiCamera)
{
    storyText.ForceMeshUpdate();

    float leastDistance = float.MaxValue;
    int counter = 0;
    bool getIndex = false;

    int currentTextLineCount = storyText.textInfo.lineCount;
    float[] listOfDistances = new float[currentTextLineCount];

    // Get the current page index (assuming you're using TMP pagination)
    int currentPage = storyText.pageToDisplay - 1;
    Debug.Log("currentPage: " + currentPage);

   


    // Get the first and last visible character index of the current page
    int firstCharIndex = storyText.textInfo.pageInfo[currentPage].firstCharacterIndex;
    int lastCharIndex = storyText.textInfo.pageInfo[currentPage].lastCharacterIndex;

     if (lastCharIndex == 0)
            lastCharIndex = storyText.textInfo.characterCount;

    for (int i = 0; i < currentTextLineCount; i++)
    {
        TMP_LineInfo lineInfo = storyText.textInfo.lineInfo[i];

        // Ensure the line's first character is within the visible page range
        if (lineInfo.firstCharacterIndex < firstCharIndex || lineInfo.firstCharacterIndex > lastCharIndex)
        {
            listOfDistances[i] = -1; // Mark it as out of bounds (not visible)
            continue;
        }

        // Get trimmed string for the line
        string s = storyText.text.Substring(lineInfo.firstCharacterIndex, lineInfo.characterCount).Trim((char)8203).Trim();
        Debug.Log("string: " + s);

        if (s.Length > 0)
        {
            // Convert world character position to screen space
            UnityEngine.Vector3 lineScreenPos = RectTransformUtility.WorldToScreenPoint(
                uiCamera,
                (storyText.transform.TransformPoint(storyText.textInfo.characterInfo[lineInfo.firstCharacterIndex].bottomLeft) +
                 storyText.transform.TransformPoint(storyText.textInfo.characterInfo[lineInfo.firstCharacterIndex].topLeft)) / 2
            );

            float distance = UnityEngine.Vector2.Distance(screenPoint, lineScreenPos);
            listOfDistances[i] = distance;
        }
        else
        {
            listOfDistances[i] = -1; // Mark empty lines
        }
    }

    for (int i = 0; i < currentTextLineCount; i++)
    {
        float currentDistance = listOfDistances[i];

        if (listOfDistances[i] != -1) // If it's a valid visible line
        {
            counter++;
            if (currentDistance < leastDistance)
            {
                leastDistance = currentDistance;
                getIndex = true;
            }
        }

        if (listOfDistances[i] == -1 || i + 1 == currentTextLineCount) // Detect blank spaces or end of text
        {
            if (getIndex)
            {
                nearestIndexes.Clear();
                for (int j = i - 1; j >= i - counter; j--)
                {
                    nearestIndexes.Add(j);
                }
            }
            getIndex = false;
            counter = 0;
        }
    }
    Debug.Log("distances");
    for (int i = 0; i < currentTextLineCount; i++)
        Debug.Log(listOfDistances[i]);

    Debug.Log("indexes");
    for (int i = 0; i < nearestIndexes.Count; i++)
        Debug.Log(nearestIndexes[i]);
}

    
}
