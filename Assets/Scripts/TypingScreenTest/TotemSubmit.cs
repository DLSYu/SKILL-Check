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

public class TotemSubmit : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject percentage;
    [SerializeField]
    // will change to serialized list later
    private DoorManager doorObserver;
    [SerializeField]
    private TMP_InputField freeformField;
    [SerializeField]
    private TMP_InputField somebodyField, wantedField, butField, soField, thenField;
    [SerializeField]
    private TypingPanel typingPanelData; // To get writing style

    private AndroidJavaClass bertScoreEval;

    private bool submitable = true;

    private string completeText;
    private string keyWord;
    private float precision;
    private float recall;
    private float f1;


    void Start()
    {
        bertScoreEval = new AndroidJavaClass("com.skillcheck.bertscore_aar.BertScoreEval");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!submitable) return;

        // Check if the object being dragged has the DraggableRelic component
        DraggableRelic draggable = eventData.pointerDrag?.GetComponent<DraggableRelic>();
        if (draggable == null)
        {
            // If the object is not a valid draggable object, ignore the drop
            return;
        }

        //percentage.SetActive(true);
        float score = EvaluateScore();


        if (Application.platform == RuntimePlatform.LinuxEditor ||
            Application.platform == RuntimePlatform.OSXEditor ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            percentage.GetComponent<TextMeshProUGUI>().text = score.ToString();
            percentage.SetActive(true);

            if (score >= 0.5f)
            {
                this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                doorObserver.GetCurrentDoor().unlockDoor();
                doorObserver.SetNextDoor();
            }
            else
            {
                this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            }
        }
    }

    private float EvaluateScore()
    {
        float score = 0.0f;

        // temporary score
        score = 0.4f;

        // Score evluation logic here
        completeText = "";

        // Get text from input field
        if (typingPanelData.GetCurrentWritingStyle() == writingStyle.freeform)
        {
            completeText = freeformField.text;
        }
        else if (typingPanelData.GetCurrentWritingStyle() == writingStyle.swbst)
        {
            completeText = somebodyField.text + " " + wantedField.text + " " +
                            butField.text + " " + soField.text + " " + thenField.text;
        }

        string referenceText = doorObserver.GetCurrentDoor().referenceText;
        keyWord = doorObserver.GetCurrentDoor().keyWord;

        // String logic here
        if (completeText.Contains(keyWord))
        {
            score += 0.1f;
            Debug.Log("KeyWord Bonus Points");
        }

        Debug.Log("Written: " + completeText + "\n" +
                "Reference: " + referenceText);

        if(Application.platform == RuntimePlatform.Android)
        {
            List<string> candidatesText = Regex.Split(completeText, @"(?<=[\.!\?])\s+").ToList<string>();
            List<string> referencesText = Regex.Split(referenceText, @"(?<=[\.!\?])\s+").ToList<string>();

            CallBertScoreEval(candidatesText, referencesText, score);

            candidatesText.Clear();
            referencesText.Clear();
        }

            return score;
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

        percentage.SetActive(true);
        bertScoreEval.CallStatic("evaluate", javaCandidates, javaReferences, new BertCallback(percentage, currScore, (value) => { submitable = value; }, (score, toAdd) => { ShowScore(score, toAdd); }));
    }

    private void ShowScore(float score, float toAdd)
    {
        Debug.Log($"score = {score}; toAdd = {toAdd}");

        //percentage.SetActive(true);
        percentage.GetComponent<TextMeshProUGUI>().text = $"{score + toAdd}";

        if (score + toAdd >= 0.5f)
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            doorObserver.GetCurrentDoor().unlockDoor();
            doorObserver.SetNextDoor();
        }
        else
        {
            this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

    }

    public class BertCallback : AndroidJavaProxy
    {
        private GameObject percentage;
        private float score;
        private Action<bool> setSubmitable;
        private Action<float, float> showScore;

        public BertCallback(GameObject percentage, float score, Action<bool> setSubmitable, Action<float, float> showScore) : base("com.skillcheck.bertscore_aar.BertScoreEval$BertCallback")
        {
            //this.precision = precision;
            //this.recall = recall;
            //this.f1 = f1;

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
            //percentage.GetComponent<TextMeshProUGUI>().text = $"{score + f1}";

            showScore(score, f1/2);

            setSubmitable(true);
        }
        public void onError(String error)
        {
            Debug.Log($"ERROR IN UNITY: {error}");
        }

    }
}







