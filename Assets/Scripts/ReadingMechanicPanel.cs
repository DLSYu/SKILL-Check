using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;


public class ReadingMechanicPanel : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI storyText;

    [TextArea]
    public string fullText;

    [SerializeField]
    private UnityEngine.UI.Button previousRelic;

     [SerializeField]
    private UnityEngine.UI.Button nextRelic;

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private LineSelector lineSelector;

    [SerializeField]
    private GameObject pagePrefab;

    [SerializeField]
    private GameObject contentHolder;

    private List<int> currentAppliedLines = new List<int>();
    private int currentSentence = 1;
    

    private List<GameObject> pagePrefabList = new List<GameObject>();
    


    void Start()
    {
        storyText.text = fullText;
        storyText.ForceMeshUpdate();

        UpdateEnabledButtons();

        for (int i = 0; i < storyText.textInfo.pageCount; i++)
        {
            pagePrefabList.Add(Instantiate(pagePrefab, contentHolder.transform)); 
        }
        ChangePagePrefab(0);
        currentSentence = 1;
        
    }

    public void PreviousLine()
    {
        if (lineSelector.SetSliderToNthSentence(currentSentence - 1) == 0)
            currentSentence -= 1;
    }
    public void NextLine()
    {
        if (lineSelector.SetSliderToNthSentence(currentSentence + 1) == 0)
            currentSentence += 1;
        
    }

    void HighlightFirstLineOfNextPage()
    {  
        int lastLine = ReturnLastLine(storyText.pageToDisplay-1);

        bool found = false;
        for (int i = 0; i < currentAppliedLines.Count && !found; i++)
        {
            Debug.Log(currentAppliedLines[i]);
            if (lastLine == currentAppliedLines[i])
            {
                found = true;
            }
        }

        currentAppliedLines.Clear();

        if (found)
        {
            int firstLine = ReturnFirstLine(storyText.pageToDisplay);
            if (storyText.textInfo.lineInfo[firstLine].characterCount != 0)
            {   
                int i = 0;
                bool hasHighlighted = false;
                storyText.ForceMeshUpdate();
                while(storyText.text.Substring(storyText.textInfo.lineInfo[firstLine+i].firstCharacterIndex, storyText.textInfo.lineInfo[firstLine+i].characterCount).Trim((char)8203).Trim().Length != 0 || !hasHighlighted)
                {
                    hasHighlighted = true;
                    ColorLine(firstLine+i, Color.yellow);
                    currentAppliedLines.Add(firstLine+i);
                    i++;
                }
                currentAppliedLines.Sort();
            }
        }
    }


    int ReturnLastLine(int page)
    {
        int lastCharIndex = storyText.textInfo.pageInfo[page - 1].lastCharacterIndex;

        // Handle cases where the lastCharIndex is 0
        if (lastCharIndex == 0)
            lastCharIndex = storyText.textInfo.characterCount - 1;

        // Find the last line that contains this character
        int lastLine = -1;
        for (int i = 0; i < storyText.textInfo.lineCount; i++)
        {
            TMP_LineInfo lineInfo = storyText.textInfo.lineInfo[i];

            if (lineInfo.lastCharacterIndex >= lastCharIndex)
            {
                lastLine = i;
                break; // We found the last line of the page
            }
        }

        return lastLine;
    }
    int ReturnFirstLine(int page)
    {
        int firstCharIndex = storyText.textInfo.pageInfo[page - 1].firstCharacterIndex;

        // Find the last line that contains this character
        int firstLine = -1;
        for (int i = 0; i < storyText.textInfo.lineCount; i++)
        {
            TMP_LineInfo lineInfo = storyText.textInfo.lineInfo[i];

            if (lineInfo.firstCharacterIndex >= firstCharIndex)
            {
                firstLine = i;
                break; // We found the last line of the page
            }
        }
        // check if last line is highlighted
            // if so, check if there's text on the next line

        return firstLine;
    }
    void HighlightLastLineOfPreviousPage()
    {
        int firstLine = ReturnFirstLine(storyText.pageToDisplay+1);
        bool found = false;
        for (int i = 0; i < currentAppliedLines.Count && !found; i++)
        {
            if (firstLine == currentAppliedLines[i])
            {
                found = true;
            }
        }

        currentAppliedLines.Clear();

        if (found)
        {
            int lastLine = ReturnLastLine(storyText.pageToDisplay);
            if (storyText.textInfo.lineInfo[lastLine].characterCount != 0)
            {   
                int i = 0;
                storyText.ForceMeshUpdate();
                while(storyText.text.Substring(storyText.textInfo.lineInfo[lastLine-i].firstCharacterIndex, storyText.textInfo.lineInfo[lastLine-i].characterCount).Trim((char)8203).Trim().Length != 0)
                {
                    ColorLine(lastLine-i, Color.yellow);
                    currentAppliedLines.Add(lastLine-i);
                    i++;
                }
                currentAppliedLines.Sort();
            }
        }
    }



    public void NextPage()
    {
        if (storyText.pageToDisplay < storyText.textInfo.pageCount)
        {
            storyText.ForceMeshUpdate();
            storyText.pageToDisplay = storyText.pageToDisplay + 1;

            UpdateEnabledButtons();
            HighlightFirstLineOfNextPage();
            lineSelector.ResetSliderToFirstLine();
            ChangePagePrefab(storyText.pageToDisplay-1);
            currentSentence = 1;
        }
    

    }

    public void PreviousPage()
    {
        if (storyText.pageToDisplay - 1 > 0)
        {
            storyText.ForceMeshUpdate();
            storyText.pageToDisplay = storyText.pageToDisplay - 1;

            UpdateEnabledButtons();
            HighlightLastLineOfPreviousPage();
            lineSelector.ResetSliderToFirstLine();
            ChangePagePrefab(storyText.pageToDisplay-1);
            currentSentence = 1;
        }
    }

    void UpdateEnabledButtons()
    {
         if (storyText.pageToDisplay == 1)
            previousRelic.enabled = false;
        
        else
            previousRelic.enabled = true;

        if (storyText.pageToDisplay == storyText.textInfo.pageCount)
            nextRelic.enabled = false;

        else
            nextRelic.enabled = true;
    }

    void ChangePagePrefab(int currentPage)
    {
        for (int i = 0; i < pagePrefabList.Count; i++)
        {
            if (i != currentPage)
            {
                pagePrefabList[i].GetComponent<UnityEngine.UI.Image>().color = new Color32(255,255,225,20);
            }

            else
            {
                pagePrefabList[i].GetComponent<UnityEngine.UI.Image>().color = new Color32(255,255,225,100);
            }
        }
    }

    void ColorLine(int lineIndex, Color color)
    {
        TMP_TextInfo textInfo = storyText.textInfo;


        TMP_LineInfo lineInfo = textInfo.lineInfo[lineIndex];

        for (int i = lineInfo.firstCharacterIndex; i <= lineInfo.lastCharacterIndex; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue; // Skip hidden characters

            int meshIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;

            // Apply color to all four vertices of the character
            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;

            
        }

        // Apply the modified colors
        storyText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    public void ApplyColorLine()
    {

        if (currentAppliedLines.Count == 0) // nothing highlighted yet
        {
             for (int i = 0; i < lineSelector.nearestIndexes.Count; i++)
                ColorLine(lineSelector.nearestIndexes[i], Color.yellow);

            currentAppliedLines = new List<int>(lineSelector.nearestIndexes);
        }
        
        else if (!IsColoredLineIndexesSame(currentAppliedLines, lineSelector.nearestIndexes))
        // something has been already highlighted, and user wants to highlight something else
         {
            storyText.ForceMeshUpdate();
            currentAppliedLines.Clear();
        
           for (int i = 0; i < lineSelector.nearestIndexes.Count; i++)
                ColorLine(lineSelector.nearestIndexes[i], Color.yellow);

            currentAppliedLines = new List<int>(lineSelector.nearestIndexes);

           
        }
        else if (currentAppliedLines.Count != 0 && IsColoredLineIndexesSame(currentAppliedLines, lineSelector.nearestIndexes))
        // line indexes are the same
        {
            storyText.ForceMeshUpdate();
            currentAppliedLines.Clear();
        }
 
    }

    bool IsColoredLineIndexesSame(List<int> list1, List<int> list2)
    {
        if (list1.Count == list2.Count)
        {
            for(int i = 0; i < list1.Count; i++)
            {
                if(list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    



  
}