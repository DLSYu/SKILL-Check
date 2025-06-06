using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighOrderStageAnalytics
{
    [SerializeField] private string dateTimeStart;
    [SerializeField] private string dateTimeEnd;
    [SerializeField] private string stageName;
    [SerializeField] private float timeTakenInInventory = 0;
    [SerializeField] private float timeTakenPeekingInInventory = 0;
    [SerializeField] private float timeTakenPeekingInStory = 0;
    [SerializeField] private float clearTime;
    [SerializeField] private List<string> submittedAnswers;
    [SerializeField] private int mistakeSomebody;
    [SerializeField] private int mistakeWanted;
    [SerializeField] private int mistakeBut;
    [SerializeField] private int mistakeSo;
    [SerializeField] private int mistakeThen;

    [SerializeField] private List<string> swbstOrFreeformList;
    [SerializeField] private List<float> scoresList;
    [SerializeField] private int gemsCollected = 0;

    public HighOrderStageAnalytics(string dateTimeStart, string stageName)
    {
        this.dateTimeStart = dateTimeStart;
        this.stageName = stageName;
    }
    public void FinishedStage(float clearTime, string dateTimeEnd)
    {
        this.clearTime = clearTime;
        this.dateTimeEnd = dateTimeEnd;
    }
    public void SetTimeTakenInInventory(float time)
    {
        this.timeTakenInInventory = time;
    }
    public void SetTimeTakenPeekingInInventory(float time)
    {
        this.timeTakenPeekingInInventory = time;
    }
    public void SetTimeTakenPeekingInStory(float time)
    {
        this.timeTakenPeekingInStory = time;
    }
    public void AddSubmittedAnswers(string answer)
    {
        this.submittedAnswers.Add(answer);
    }
    public void AddSwbstOrFreeform(string type)
    {
        this.swbstOrFreeformList.Add(type);
    }

    public void AddScores(float score)
    {
        this.scoresList.Add(score);
    }

    public void IncrementGemsCollected()
    {
        this.gemsCollected++;
    }



}
