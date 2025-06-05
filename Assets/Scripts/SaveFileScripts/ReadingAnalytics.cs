using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingAnalytics
{
    private string dateTimeStart;
    private string stageName;
    private int timeTakenReading;
    private string dateTimeEnd;
    private List<string> dictionaryWordsClicked;

    public ReadingAnalytics(string dateTimeStart, string stageName)
    {
        this.dateTimeStart = dateTimeStart;
        this.stageName = stageName;

    }

    public void SetDateTimeEnd(System.DateTime dateTime)
    {
        this.dateTimeEnd = dateTime.ToString();
    }


    public void SetTimeTakenReading(int time)
    {
        this.timeTakenReading = time;
    }



}
