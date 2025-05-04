using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighOrderStageAnalytics
{
    private string dateTimeStart;
    private string stageNumber;
    private int timeTakenReading;
    private int timeTakenInInventory;
    private int timeTakenPeekingInInventory;
    private int timeTakenPeekingInStory;
    private int clearTime;
    private List<int> timeTakenComposingList;
    private List<string> submittedSentences;
    private List<string> swbstOrFreeformList;
    private List<float> scoresList;
    private int gemsCollected;

    public void SetStageNumber(int number)
    {
        this.stageNumber = "Stage " + number.ToString();
    }

    public void SetTimeTakenReading(int time)
    {
        this.timeTakenReading = time;
    }

    public void SetTimeTakenInInventory(int time)
    {
        this.timeTakenInInventory = time;
    }
    public void SetTimeTakenPeekingInInventory(int time)
    {
        this.timeTakenPeekingInInventory = time;
    }
    public void SetTimeTakenPeekingInStory(int time)
    {
        this.timeTakenPeekingInStory = time;
    }
    public void SetClearTime(int time)
    {
        this.clearTime = time;
    }
    public void AddTimeTakenComposing(int time)
    {
        this.timeTakenComposingList.Add(time);
    }
    public void SetSubmittedSentences(string sentence)
    {
        this.submittedSentences.Add(sentence);
    }
    public void swbstOrFreeform(string type)
    {
        this.swbstOrFreeformList.Add(type);
    }

    public void AddScores(float score)
    {
        this.scoresList.Add(score);
    }

    public void SetGemsCollected(int gems)
    {
        this.gemsCollected = gems;
    }



}
