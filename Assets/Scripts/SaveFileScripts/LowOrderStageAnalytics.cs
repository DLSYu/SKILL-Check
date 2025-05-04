using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowOrderStageAnalytics
{
    private string dateTimeStart;
    private string stageNumber;
    private int timeTakenReading;
    private int clearTime;
    private int mistakes;

    public void SetStageNumber(int number)
    {
        this.stageNumber = "Stage " + number.ToString();
    }

    public void SetTimeTakenReading(int time)
    {
        this.timeTakenReading = time;
    }

    public void SetClearTime(int time)
    {
        this.clearTime = time;
    }

    public void SetMistakes(int mistakes)
    {
        this.mistakes = mistakes;
    }

}
