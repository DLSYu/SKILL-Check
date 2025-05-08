using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupOnStageStart : MonoBehaviour
{
    [SerializeField]
    private bool isHighOrder, isLowOrder;
    [SerializeField]
    private statueStage statueStage = statueStage.Not_Statue_Stage;
    [SerializeField]
    private bookStage bookStage = bookStage.Not_Book_Stage;

    void Awake()
    {
        if (isHighOrder)
        {
            StoryData.SetCurrentHighOrderStage(statueStage);
            StoryData.currentGameMode = "HighOrder";
        }
        else if (isLowOrder)
        {
            StoryData.SetCurrentLowOrderStage(bookStage);
            StoryData.currentGameMode = "LowOrder";
        }
    }
}
