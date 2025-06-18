using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighOrderStageAnalytics
{
    [SerializeField] private string dateTimeStart;
    [SerializeField] private string stageName;
    [SerializeField] private float timeTakenInInventory = 0;
    [SerializeField] private float timeTakenInStory = 0;
    [SerializeField] private float timeTakenPeekingInInventory = 0;
    [SerializeField] private float timeTakenPeekingInStory = 0;

    [SerializeField] private float clearTime;
    [SerializeField] private int gemsCollected = 0;

    [SerializeField] public HighOrderStageTypeAnalytics highOrderStageTypeAnalytics;
    [SerializeField] private string dateTimeEnd;

    public void SetStartingStats(string dateTimeStart, string stageName)
    {
        this.dateTimeStart = dateTimeStart;
        this.stageName = stageName;
    }
    public void SetClearTime(float clearTime)
    {
        this.clearTime = clearTime;
    }

    public void SetDateTimeEnd(string dateTimeEnd)
    {
        this.dateTimeEnd = dateTimeEnd;
    }
    public void AddTimeTakenInInventory(float time)
    {
        this.timeTakenInInventory += time;
    }
    public void AddTimeTakenPeekingInInventory(float time)
    {
        this.timeTakenPeekingInInventory += time;
    }
    public void AddTimeTakenPeekingInStory(float time)
    {
        this.timeTakenPeekingInStory += time;
    }
    public void AddTimeTakenInStory(float time)
    {
        this.timeTakenInStory += time;
    }
    public void IncrementGemsCollected()
    {
        this.gemsCollected++;
    }



}
