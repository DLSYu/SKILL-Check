using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LowOrderStageAnalytics
{
    [SerializeField] private string dateTimeStart;
    [SerializeField] private string stageName;
    [SerializeField] private float clearTime;
    [SerializeField] private int mistakes = 0;
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

    public void IncrementMistakes()
    {
        this.mistakes++;
    }

}
