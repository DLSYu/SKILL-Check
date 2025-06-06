using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReadingAnalytics
{
    [SerializeField] private string dateTimeStart;
    [SerializeField] private string stageName;
    [SerializeField] private float timeTakenReading;
    [SerializeField] private string dateTimeEnd;
    [SerializeField] private List<string> dictionaryWordsClicked = new List<string>();
    [SerializeField] private bool continuedToGame = false;

    public void SetStartingStats(string dateTimeStart, string stageName)
    {
        this.dateTimeStart = dateTimeStart;
        this.stageName = stageName;

    }

    public void SetDateTimeEnd(string dateTime)
    {
        this.dateTimeEnd = dateTime;
    }


    public void AddTimeTakenReading(float time)
    {
        this.timeTakenReading += time;
    }

    public void AddNewDictionaryWord(string word)
    {
        dictionaryWordsClicked.Add(word);
    }

    public void SetContinuedToGame()
    {
        continuedToGame = true;
    }
    public bool GetContinuedToGame()
    {
        return continuedToGame;
    }



}
