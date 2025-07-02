using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;


//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GateSubmit : MonoBehaviour
{


    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI percentage;
    [SerializeField]
    // will change to serialized list later
    private DoorManager doorObserver;
    [SerializeField]
    private UIManagerTemplate uIManagerTemplate;
    [SerializeField]
    private TMP_InputField freeformField;
    [SerializeField]
    private TMP_InputField somebodyField, wantedField, butField, soField, thenField;
    [SerializeField]
    private TypingPanel typingPanelData; // To get writing style

    [Header("Results Panel")]
    [SerializeField]
    private TextMeshProUGUI resultsScoreText;
    [SerializeField]
    private TextMeshProUGUI resultsResultsText;
    [SerializeField]
    private GameObject resultsPanelHolder;

    [SerializeField]
    private GameObject fairyMistakeGems; // for low SWBST

    [Header("Early Level Scoring")]
    [SerializeField] private TextMeshProUGUI scoreText; // Assign SCORETEXT object in inspector
    [SerializeField] private GameObject scorePanelHolder; // Assign SCOREPANELHOLDER in inspector


    [SerializeField]
    private GameObject loadingEvaluatingScreen;
    [Header("For Early level submission")]
    [SerializeField]
    private SWBSTSlot somebodySlot;
    [SerializeField]
    private SWBSTSlot wantedSlot;
    [SerializeField]
    private SWBSTSlot butSlot;
    [SerializeField]
    private SWBSTSlot soSlot;
    [SerializeField]
    private SWBSTSlot thenSlot;
    private AndroidJavaClass bertScoreEval;

    [SerializeField]
    private GameObject fairyMissingFieldHandler;

    private bool submitable = true;

    private string completeText;
    private string keyWord;
    private float precision;
    private float recall;
    private float f1;

    private float passingScore = 0.63f;
    private float bonusKeywordScore = 0.05f;


    void Start()
    {
        bertScoreEval = new AndroidJavaClass("com.skillcheck.bertscore_aar.BertScoreEval");
    }

    public void OnSubmitButton()
    {
        if (!submitable) return;
        loadingEvaluatingScreen.SetActive(true);
        //percentage.SetActive(true);
        float score = EvaluateScore();




        if (Application.platform == RuntimePlatform.LinuxEditor ||
                    Application.platform == RuntimePlatform.OSXEditor ||
                    Application.platform == RuntimePlatform.WindowsEditor)
        {


            if (score >= passingScore)
            {
                // resultsResultsText.text = "Cleared!";
                //this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                doorObserver.GetCurrentDoor().unlockDoor();
                doorObserver.SetNextDoor();
            }
            else
            {
                // resultsResultsText.text = "Try again!";
                //this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }


            if (score == -1)
            {
                fairyMissingFieldHandler.SetActive(true);
            }
            else
            {
                percentage.text = "Score: " + (score * 100).ToString();
                resultsScoreText.text = "Score: " + (score * 100).ToString();
                resultsPanelHolder.SetActive(true);
            }

            loadingEvaluatingScreen.SetActive(false);


        }
    }

    public void DismissResultsScreen()
    {
        resultsPanelHolder.SetActive(false);
    }

    private float EvaluateScore()
    {
        float score = 0.0f;

        // temporary score
        score = 0.4f;

        // Score evluation logic here
        completeText = "";

        bool areAllFieldsFilled = true;

        HO_SubmitAttempt hO_SubmitAttempt = new HO_SubmitAttempt();

        // Get text from input field
        if (typingPanelData.GetCurrentWritingStyle() == writingStyle.freeform)
        {
            completeText = freeformField.text;

            areAllFieldsFilled = CheckIfFieldIsNotEmpty(completeText);

            if (areAllFieldsFilled)
            {
                HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.AddSWBSTOrFreeformList(SWBSTOrFreeform.Freeform);
                hO_SubmitAttempt.submittedAnswers.Add(completeText);
            }
        }
        else if (typingPanelData.GetCurrentWritingStyle() == writingStyle.swbst)
        {

            // NOTE: It is important to set disabled fields (or pre-filled slots) as null in GateSubmit
            if (somebodyField != null)
            {
                hO_SubmitAttempt.submittedAnswers.Add(somebodyField.text);
                completeText += somebodyField.text + ". ";
                areAllFieldsFilled = areAllFieldsFilled && CheckIfFieldIsNotEmpty(somebodyField.text);
            }

            if (wantedField != null)
            {
                hO_SubmitAttempt.submittedAnswers.Add(wantedField.text);
                completeText += wantedField.text + ". ";
                areAllFieldsFilled = areAllFieldsFilled && CheckIfFieldIsNotEmpty(wantedField.text);
            }

            if (butField != null)
            {
                hO_SubmitAttempt.submittedAnswers.Add(butField.text);
                completeText += butField.text + ". ";
                areAllFieldsFilled = areAllFieldsFilled && CheckIfFieldIsNotEmpty(butField.text);
            }
            if (soField != null)
            {
                hO_SubmitAttempt.submittedAnswers.Add(soField.text);
                completeText += soField.text + ". ";
                areAllFieldsFilled = areAllFieldsFilled && CheckIfFieldIsNotEmpty(soField.text);
            }

            if (thenField != null)
            {
                hO_SubmitAttempt.submittedAnswers.Add(thenField.text);
                completeText += thenField.text + ". ";
                areAllFieldsFilled = areAllFieldsFilled && CheckIfFieldIsNotEmpty(thenField.text);
            }

            if (areAllFieldsFilled)
                HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.AddSWBSTOrFreeformList(SWBSTOrFreeform.SWBST);
        }

        if (areAllFieldsFilled)
        {


            HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.AddAnswer(hO_SubmitAttempt);

            String[] doorData = doorObserver.GetCurrentDoor().getDoorData();

            string referenceText = doorData[0];
            keyWord = doorData[1];

            Debug.Log("Reference Text: " + referenceText);
            Debug.Log("Keyword: " + keyWord);

            // String logic here

            Debug.Log("completeText: " + completeText);
            if (completeText.Contains(keyWord) || completeText.Contains(keyWord.ToLower()))
            {
                score += bonusKeywordScore;
                Debug.Log("KeyWord Bonus Points");

                if (Application.platform == RuntimePlatform.WindowsEditor ||
                    Application.platform == RuntimePlatform.LinuxEditor ||
                    Application.platform == RuntimePlatform.OSXEditor)
                {
                    score += 1.0f; // Add 1.0f for bonus keyword score in editor platforms
                    Debug.Log("Added 1.0f for bonus keyword score for Testing purposes");
                }
            }

            if (Application.platform == RuntimePlatform.Android)
            {
                List<string> candidatesText = Regex.Split(completeText, @"(?<=[\.!\?])\s+").ToList<string>();
                List<string> referencesText = Regex.Split(referenceText, @"(?<=[\.!\?])\s+").ToList<string>();

                if (candidatesText.Count > 1)
                    candidatesText.RemoveAt(Regex.Split(completeText, @"(?<=[\.!\?])\s+").ToList<string>().Count - 1);



                CallBertScoreEval(candidatesText, referencesText, score);

                candidatesText.Clear();
                referencesText.Clear();
            }



            HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.AddScore(score);
            return score;
        }
        else
        {
            loadingEvaluatingScreen.SetActive(false);
            fairyMissingFieldHandler.SetActive(true);
            return -1;
        }


    }

    private bool CheckIfFieldIsNotEmpty(string answer)
    {
        string input = answer.Trim((char)8203);

        if (input.Length == 0)
            return false;
        else
            return true;
    }

    private void CallBertScoreEval(List<string> candidates, List<string> references, float currScore)
    {
        AndroidJavaObject javaCandidates = new AndroidJavaObject("java.util.ArrayList");
        foreach (string candidate in candidates)
        {
            javaCandidates.Call<bool>("add", candidate);
        }

        AndroidJavaObject javaReferences = new AndroidJavaObject("java.util.ArrayList");
        foreach (string reference in references)
        {
            javaReferences.Call<bool>("add", reference);
        }

        bertScoreEval.CallStatic("evaluate", javaCandidates, javaReferences,
    new BertCallback(
        this,
        percentage,
        currScore,
        (value) => { submitable = value; },
        (score, toAdd) =>
        {
            StartCoroutine(RunOnMainThread(() =>
            {
                ShowScore(score, toAdd);
                loadingEvaluatingScreen.SetActive(false);
                resultsPanelHolder.SetActive(true);
            }));
        }));
    }

    private void ShowScore(float score, float toAdd)
    {
        Debug.Log($"score = {score}; toAdd = {toAdd}");

        //percentage.SetActive(true);
        percentage.text = "Score: " + $"{(score + toAdd) * 100}";
        resultsScoreText.text = "Score: " + $"{(score + toAdd) * 100}";

        if (score + toAdd >= passingScore)
        {
            // this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            resultsResultsText.text = "Cleared!";
            doorObserver.GetCurrentDoor().unlockDoor();
            doorObserver.SetNextDoor();
        }
        else
        {
            resultsResultsText.text = "Try again!";
            // this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

    }

    public void CheckIfDismissTypingPanel()
    {
        if (resultsResultsText.text == "Cleared!")
        {
            uIManagerTemplate.exitTypingScreen();
        }
    }

    public void clearAllfields()
    {
        freeformField.text = "";
        somebodyField.text = "";
        wantedField.text = "";
        butField.text = "";
        soField.text = "";
        thenField.text = "";
    }

    public IEnumerator RunOnMainThread(Action action)
    {
        yield return null; // Wait one frame to make sure we're on Unity's main thread
        action?.Invoke();
    }

    public class BertCallback : AndroidJavaProxy
    {
        private TextMeshProUGUI percentage;
        private float score;
        private Action<bool> setSubmitable;
        private Action<float, float> showScore;

        private GateSubmit gateSubmit;

        public BertCallback(GateSubmit gateSubmit, TextMeshProUGUI percentage, float score, Action<bool> setSubmitable, Action<float, float> showScore)
            : base("com.skillcheck.bertscore_aar.BertScoreEval$BertCallback")
        {
            this.gateSubmit = gateSubmit;
            this.percentage = percentage;
            this.score = score;
            this.setSubmitable = setSubmitable;
            this.showScore = showScore;
        }

        public void sendResult(AndroidJavaObject results)
        {
            Debug.Log("Returned to Unity...");

            int size = results.Call<int>("size");
            List<string> scores = new List<string>();
            for (int i = 0; i < size; i++)
            {
                scores.Add(results.Call<string>("get", i));
            }

            float f1 = float.Parse(scores[2], CultureInfo.InvariantCulture.NumberFormat);

            gateSubmit.StartCoroutine(gateSubmit.RunOnMainThread(() =>
            {
                showScore(score, f1 / 2);
                setSubmitable(true);
            }));
        }
        public void onError(String error)
        {
            Debug.Log($"ERROR IN UNITY: {error}");
        }

    }

    public void OnSubmitEarlyLevelButton()
    {
        int correctCount = 0;
        List<Gem_Early> incorrectGems = new List<Gem_Early>();

        // Check slots and collect incorrect gems with debug logs
        if (CheckSlot(somebodySlot, "Somebody", ref correctCount, incorrectGems) &&
        CheckSlot(wantedSlot, "Wanted", ref correctCount, incorrectGems) &&
        CheckSlot(butSlot, "But", ref correctCount, incorrectGems) &&
        CheckSlot(soSlot, "So", ref correctCount, incorrectGems) &&
        CheckSlot(thenSlot, "Then", ref correctCount, incorrectGems))
        {

            Debug.Log($"Found {incorrectGems.Count} incorrect gems");

            ResetAllHighlights();

            // Wait then Highlight
            StartCoroutine(HighlightIncorrectGemsAfterReset(incorrectGems));

            // Update score display
            scoreText.text = $"Score: {correctCount}/5";
            scorePanelHolder.SetActive(true); // Show the score panel

            UpdateResetButtonsVisibility();

            // Unlock door if all correct
            if (correctCount == 5)
            {
                // Hide Typing Panel
                uIManagerTemplate.exitTypingScreen();

                doorObserver.GetCurrentDoor().unlockDoor();
                doorObserver.SetNextDoor();
            }

            else
            {

                if (!somebodySlot.compareGemTypeToSlotType())
                {
                    HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.earlyStage.IncrementMistakeSomebody();
                }
                if (!wantedSlot.compareGemTypeToSlotType())
                {
                    HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.earlyStage.IncrementMistakeWanted();
                }
                if (!butSlot.compareGemTypeToSlotType())
                {
                    HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.earlyStage.IncrementMistakeBut();
                }
                if (!soSlot.compareGemTypeToSlotType())
                {
                    HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.earlyStage.IncrementMistakeSo();
                }
                if (!thenSlot.compareGemTypeToSlotType())
                {
                    HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics.earlyStage.IncrementMistakeThen();
                }


            }
        }
        else
        {
            fairyMissingFieldHandler.SetActive(true);
        }
    }

    private void UpdateResetButtonsVisibility()
    {
        somebodySlot.SetCorrectness(somebodySlot.compareGemTypeToSlotType());
        wantedSlot.SetCorrectness(wantedSlot.compareGemTypeToSlotType());
        butSlot.SetCorrectness(butSlot.compareGemTypeToSlotType());
        soSlot.SetCorrectness(soSlot.compareGemTypeToSlotType());
        thenSlot.SetCorrectness(thenSlot.compareGemTypeToSlotType());
    }

    private IEnumerator HighlightIncorrectGemsAfterReset(List<Gem_Early> incorrectGems)
    {
        if (incorrectGems.Count > 0)
            fairyMistakeGems.SetActive(true);
        // Wait for the reset to complete
        yield return new WaitForEndOfFrame();

        // Now highlight the incorrect gems
        foreach (var gem in incorrectGems)
        {
            if (gem != null)
            {
                Debug.Log($"Highlighting gem: {gem.name} with keyword: {gem.Keyword}");
                RelicPopupHandler popup = gem.GetComponent<RelicPopupHandler>();
                if (popup != null)
                {
                    popup.SetShouldHighlight(true);
                    Debug.Log($"Set highlight flag for gem: {gem.name}");
                }
                else
                {
                    Debug.LogWarning($"No RelicPopupHandler found on gem: {gem.name}");
                }
            }
        }
    }

    private void ResetAllHighlights()
    {
        // Reset highlights for all gems in all slots
        ResetSlotHighlight(somebodySlot);
        ResetSlotHighlight(wantedSlot);
        ResetSlotHighlight(butSlot);
        ResetSlotHighlight(soSlot);
        ResetSlotHighlight(thenSlot);
    }

    private void ResetSlotHighlight(SWBSTSlot slot)
    {
        if (slot.GetCurrentGem() != null)
        {
            RelicPopupHandler popup = slot.GetCurrentGem().GetComponent<RelicPopupHandler>();
            if (popup != null)
            {
                popup.SetShouldHighlight(false);
            }
        }
    }

    private bool CheckSlot(SWBSTSlot slot, string slotName, ref int correctCount, List<Gem_Early> incorrectGems)
    {
        if (slot.compareGemTypeToSlotType())
        {
            correctCount++;
            Debug.Log($"{slotName} slot is correct");
            return true;
        }
        else if (slot.GetCurrentGem() != null)
        {
            incorrectGems.Add(slot.GetCurrentGem());
            slot.ResetSlot();
            Debug.Log($"{slotName} slot is incorrect");
            return true;
        }
        else
        {
            Debug.Log($"{slotName} slot is empty");
            return false;
        }
    }

    public void OnSubmitMidLevelButton()
    {
        // validation logic here



        OnSubmitButton();
    }
}