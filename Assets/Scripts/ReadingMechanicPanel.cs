using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;

public class ReadingMechanicPanel : MonoBehaviour, IDataPersistence
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
    [SerializeField]
    private TextMeshInputHelper textInputHelper;
    [SerializeField]
    private GameObject DictionaryPanel;

    [SerializeField]
    private GameObject helpPanel;
    [SerializeField]
    private AudioSource voiceActing;
    [SerializeField]
    private VoiceManager voiceManager;
    [SerializeField]
    private GameObject tutorialHandler;

    private bool hasTutorialPlayed;

    private List<int> currentAppliedLines = new List<int>();
    private List<GameObject> pagePrefabList = new List<GameObject>();
    private bool isVoicePlaying = false;

    void Awake()
    {
        // if story data not null, get the story string
        if (!string.IsNullOrEmpty(StoryData.GetStoryString()))
        {
            fullText = StoryData.GetStoryString();
        }

        storyText.text = fullText;
        storyText.ForceMeshUpdate();
    }
    void Start()
    {
        UpdateEnabledButtons();

        for (int i = 0; i < storyText.textInfo.pageCount; i++)
        {
            pagePrefabList.Add(Instantiate(pagePrefab, contentHolder.transform));
        }
        ChangePagePrefab(0);
        lineSelector.currentSentenceIndex = 1;

        if (tutorialHandler != null)
        {
            if (!hasTutorialPlayed)
            {
                tutorialHandler.SetActive(true);
            }
        }

    }
    public void LoadData(GameData data)
    {
        data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("ReadingMechanicTutorial", out hasTutorialPlayed);
    }
    public void SaveData(GameData data)
    {

    }

    void RemoveHelpPanel()
    {
        if (helpPanel.activeInHierarchy)
            helpPanel.SetActive(false);
    }

    public void ToggleHelpPanel()
    {
        if (helpPanel.activeInHierarchy)
        {
            helpPanel.SetActive(false);
        }
        else
        {
            helpPanel.SetActive(true);
        }
    }

    public void PreviousLine()
    {
        RemoveHelpPanel();
        if (lineSelector.SetSliderToNthSentence(lineSelector.currentSentenceIndex - 1) == 0)
            lineSelector.currentSentenceIndex -= 1;
    }
    public void NextLine()
    {
        RemoveHelpPanel();
        if (lineSelector.SetSliderToNthSentence(lineSelector.currentSentenceIndex + 1) == 0)
            lineSelector.currentSentenceIndex += 1;

    }

    void HighlightFirstLineOfNextPage()
    {
        int lastLine = ReturnLastLine(storyText.pageToDisplay - 1);

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
                while (storyText.text.Substring(storyText.textInfo.lineInfo[firstLine + i].firstCharacterIndex, storyText.textInfo.lineInfo[firstLine + i].characterCount).Trim((char)8203).Trim().Length != 0 || !hasHighlighted)
                {
                    hasHighlighted = true;
                    ColorLine(firstLine + i, Color.yellow);
                    currentAppliedLines.Add(firstLine + i);
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
        int firstLine = ReturnFirstLine(storyText.pageToDisplay + 1);
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
                while (storyText.text.Substring(storyText.textInfo.lineInfo[lastLine - i].firstCharacterIndex, storyText.textInfo.lineInfo[lastLine - i].characterCount).Trim((char)8203).Trim().Length != 0)
                {
                    ColorLine(lastLine - i, Color.yellow);
                    currentAppliedLines.Add(lastLine - i);
                    i++;
                }
                currentAppliedLines.Sort();
            }
        }
    }



    public void NextPage()
    {
        RemoveHelpPanel();
        if (storyText.pageToDisplay < storyText.textInfo.pageCount)
        {
            storyText.ForceMeshUpdate();
            storyText.pageToDisplay = storyText.pageToDisplay + 1;
            textInputHelper.ActivateButtonsOnPage(storyText.pageToDisplay);

            UpdateEnabledButtons();
            HighlightFirstLineOfNextPage();
            lineSelector.ResetSliderToFirstLine();
            ChangePagePrefab(storyText.pageToDisplay - 1);
            lineSelector.currentSentenceIndex = 1;
            Debug.Log("Current Index: " + lineSelector.currentSentenceIndex + " Total from prev pages: " + GetTotalSentencesFromPreviousPages());
        }
    }

    public void PreviousPage()
    {
        RemoveHelpPanel();
        if (storyText.pageToDisplay - 1 > 0)
        {
            storyText.ForceMeshUpdate();
            storyText.pageToDisplay = storyText.pageToDisplay - 1;
            textInputHelper.ActivateButtonsOnPage(storyText.pageToDisplay);

            UpdateEnabledButtons();
            HighlightLastLineOfPreviousPage();
            lineSelector.ResetSliderToFirstLine();
            ChangePagePrefab(storyText.pageToDisplay - 1);
            lineSelector.currentSentenceIndex = 1;
            Debug.Log("Current Index: " + lineSelector.currentSentenceIndex + " Total from prev pages: " + GetTotalSentencesFromPreviousPages());
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
                pagePrefabList[i].GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 100);
            }

            else
            {
                pagePrefabList[i].GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 225, 100);
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
        RemoveHelpPanel();
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
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    public void ToggleDictionaryMode()
    {
        RemoveHelpPanel();
        // if (isDictionaryMode == false)
        // textInputHelper.ActivateButtonsOnPage(storyText.pageToDisplay);
        // else
        // deactivate all buttons in textInputHelper

        bool isDictionaryMode = textInputHelper.isDictionaryActive;

        if (isDictionaryMode == false)
        {
            textInputHelper.isDictionaryActive = true;
            textInputHelper.ActivateButtonsOnPage(storyText.pageToDisplay);
        }
        else
        {
            textInputHelper.isDictionaryActive = false;
            textInputHelper.DeactivateButtonsOnPage(storyText.pageToDisplay);
        }
    }

    private int GetTotalSentencesFromPreviousPages()
    {
        int total = 0;
        for (int page = 1; page < storyText.pageToDisplay; page++)
        {
            total += GetSentenceCountForPage(page);
        }
        return total;
    }

    private int GetSentenceCountForPage(int page)
    {
        bool start = false;
        int currentSentence = 0;

        int currentTextLineCount = storyText.textInfo.lineCount;
        int firstCharIndex = storyText.textInfo.pageInfo[page - 1].firstCharacterIndex;
        int lastCharIndex = storyText.textInfo.pageInfo[page - 1].lastCharacterIndex;

        if (lastCharIndex == 0)
            lastCharIndex = storyText.textInfo.characterCount;

        for (int i = 0; i < currentTextLineCount; i++)
        {
            TMP_LineInfo lineInfo = storyText.textInfo.lineInfo[i];
            string s = storyText.text.Substring(lineInfo.firstCharacterIndex, lineInfo.characterCount).Trim((char)8203).Trim();

            if (lineInfo.firstCharacterIndex >= firstCharIndex && !start && lineInfo.lastCharacterIndex <= lastCharIndex && s.Length != 0)
            {
                start = true;
                currentSentence++;
            }

            if (lineInfo.lastCharacterIndex > lastCharIndex)
                break;

            if (start && s.Length == 0)
            {
                start = false;
            }
        }
        return currentSentence;
    }

    public void PlayVoiceLine()
    {

        if (isVoicePlaying)
            return;

        int totalSentences = GetTotalSentencesFromPreviousPages();
        Debug.Log("should be playing: " + (lineSelector.currentSentenceIndex + totalSentences));
        TimeStamping currentIndex = voiceManager.GetTimeStamping(lineSelector.currentSentenceIndex + totalSentences);
        StartCoroutine(PlayFromTo(currentIndex.start, currentIndex.end));
        isVoicePlaying = true;
    }

    // Added is Playing checks so it doesnt bother when replaying
    private IEnumerator PlayFromTo(float startTime, float endTime)
    {

        voiceActing.Stop();

        voiceActing.time = startTime;
        voiceActing.Play();

        yield return null; // Wait one frame to ensure playback starts

        float segmentDuration = endTime - startTime;
        yield return new WaitForSeconds(segmentDuration);

        voiceActing.Stop();

        isVoicePlaying = false;
    }


}