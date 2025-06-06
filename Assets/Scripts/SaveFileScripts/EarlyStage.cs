using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EarlyStage
{
    [SerializeField] private int mistakeSomebody = 0;
    [SerializeField] private int mistakeWanted = 0;
    [SerializeField] private int mistakeBut = 0;
    [SerializeField] private int mistakeSo = 0;
    [SerializeField] private int mistakeThen = 0;

    public void IncrementMistakeSomebody()
    {
        mistakeSomebody++;
    }

    public void IncrementMistakeWanted()
    {
        mistakeWanted++;
    }
    public void IncrementMistakeBut()
    {
        mistakeBut++;
    }
    public void IncrementMistakeSo()
    {
        mistakeSo++;
    }
    public void IncrementMistakeThen()
    {
        mistakeThen++;
    }


}
