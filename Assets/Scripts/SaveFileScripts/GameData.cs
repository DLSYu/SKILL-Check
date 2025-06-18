using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class GameData
{
    public SerializedDictionary<string, bool> stageCompletionDictionary;
    public SerializedDictionary<string, bool> alreadyPlayedAnimationForNewlyOpenedStage;

    public List<LowOrderStageAnalytics> lowOrderStageAnalyticsList;
    public List<HighOrderStageAnalytics> highOrderStageAnalyticsList;

    public List<ReadingAnalytics> readingAnalyticsList;

    public List<float> swbstInLobbyTime;

    public GameData()
    {
        this.stageCompletionDictionary = new SerializedDictionary<string, bool>();
        this.alreadyPlayedAnimationForNewlyOpenedStage = new SerializedDictionary<string, bool>();
        this.lowOrderStageAnalyticsList = new List<LowOrderStageAnalytics>();
        this.highOrderStageAnalyticsList = new List<HighOrderStageAnalytics>();
        this.readingAnalyticsList = new List<ReadingAnalytics>();
    }
}
