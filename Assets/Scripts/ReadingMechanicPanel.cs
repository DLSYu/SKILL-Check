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
        Debug.Log("currentSentenceBeforeNextLine: " + currentSentence);
        if (lineSelector.SetSliderToNthSentence(currentSentence + 1) == 0)
            currentSentence += 1;
        
    }



    public void NextPage()
    {
        if (storyText.pageToDisplay < storyText.textInfo.pageCount)
        {
            storyText.ForceMeshUpdate();
            storyText.pageToDisplay = storyText.pageToDisplay + 1;

            currentAppliedLines.Clear();
            UpdateEnabledButtons();
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

            currentAppliedLines.Clear();
            UpdateEnabledButtons();
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

        if (lineIndex < 0 || lineIndex >= textInfo.lineCount)
        {
            Debug.LogWarning("Invalid line index!");
            return;
        }

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