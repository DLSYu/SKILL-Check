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

    private int currentBoldedLine = -1;
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



    public void NextPage()
    {
        RemoveBold();
        if (storyText.pageToDisplay < storyText.textInfo.pageCount)
        {
            storyText.pageToDisplay = storyText.pageToDisplay + 1;

            UpdateEnabledButtons();
            lineSelector.ResetSliderToFirstLine();
            ChangePagePrefab(storyText.pageToDisplay-1);
            currentSentence = 1;
        }
    

    }

    public void PreviousPage()
    {
        RemoveBold();
        if (storyText.pageToDisplay - 1 > 0)
        {
            storyText.pageToDisplay = storyText.pageToDisplay - 1;

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

    public void BoldWordInLine()
    {
        // Ensure we have updated text info
        storyText.ForceMeshUpdate();

        int charCount = 0;
 
        if (currentBoldedLine != lineSelector.nearestIndexes[lineSelector.nearestIndexes.Count-1]) {
            RemoveBold();

            TMP_TextInfo textInfo = storyText.textInfo;

            TMP_LineInfo lineInfo = textInfo.lineInfo[lineSelector.nearestIndexes[0]];

            for (int i = 0; i < lineSelector.nearestIndexes.Count; i++)
            {
                charCount += textInfo.lineInfo[lineSelector.nearestIndexes[i]].characterCount;
            }

            Debug.Log("charCount: " + charCount);
            
            // TO-DO: make the text inbounds

            // Extract full text from the line
            string lineText = storyText.text.Substring(lineInfo.firstCharacterIndex, charCount);

            // Replace the target word with a bold version
            string boldText = "<b>" + lineText + "</b>";
            string modifiedLine = lineText.Replace(lineText, boldText);

            currentBoldedLine = lineSelector.nearestIndexes[lineSelector.nearestIndexes.Count-1];
            


            // Replace the original line in the full text
            string modifiedText = storyText.text.Remove(lineInfo.firstCharacterIndex, charCount)
                                .Insert(lineInfo.firstCharacterIndex, modifiedLine);

            // Apply the new text
            storyText.text = modifiedText;
        }
        else {
            currentBoldedLine = -1;
            RemoveBold();
        }
    }
    void RemoveBold()
    {
        if (storyText.text.Contains("<b>"))
        {
           string cleanText = Regex.Replace(storyText.text, @"<\/?b>", "");

             // Apply the cleaned text
            storyText.text = cleanText;
        }
    }
}