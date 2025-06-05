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

    public LowOrderStageAnalytics(string dateTimeStart, string stageName)
    {
        this.dateTimeStart = dateTimeStart;
        this.stageName = stageName;
    }

    public void FinishGameTime(float clearTime, string dateTimeEnd)
    {
        this.clearTime = clearTime;
        this.dateTimeEnd = dateTimeEnd;
    }

    public void IncrementMistakes()
    {
        this.mistakes++;
    }

}
