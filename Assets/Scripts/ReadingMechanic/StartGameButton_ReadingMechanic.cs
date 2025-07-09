using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton_ReadingMechanic : MonoBehaviour
{
    [SerializeField]
    private ReadingMechanicPanel readingMechanicPanel;
    [SerializeField]
    private LoadingScreen loadingScreen;
    public void startGameScene()
    {

        ReadingAnalyticsManager.instance.readingAnalytics.SetContinuedToGame();
        readingMechanicPanel.StopVoiceLineFromPauseButton();



        if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_1)
            loadingScreen.LoadScene("H01_Early");
        else if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_2)
            loadingScreen.LoadScene("H02_Early");
        else if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_3)
            loadingScreen.LoadScene("H03_Early");
        else if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_4)
            loadingScreen.LoadScene("H04_Mid");
        else if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_5)
            loadingScreen.LoadScene("H05_Mid");
        else if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_6)
            loadingScreen.LoadScene("H06_Late");
        else if (StoryData.currentGameMode == "LowOrder" && StoryData.currentBookStage == bookStage.LO_1)
            loadingScreen.LoadScene("SortingScene");
        else if (StoryData.currentGameMode == "LowOrder" && StoryData.currentBookStage == bookStage.LO_2)
            loadingScreen.LoadScene("SortingScene2");
        else if (StoryData.currentGameMode == "LowOrder" && StoryData.currentBookStage == bookStage.LO_3)
            loadingScreen.LoadScene("SortingScene3");
        else if (StoryData.currentGameMode == "LowOrder" && StoryData.currentBookStage == bookStage.LO_4)
            loadingScreen.LoadScene("SortingScene4");
        else if (StoryData.currentGameMode == "LowOrder" && StoryData.currentBookStage == bookStage.LO_5)
            loadingScreen.LoadScene("SortingScene5");
        else if (StoryData.currentGameMode == "LowOrder" && StoryData.currentBookStage == bookStage.LO_6)
            loadingScreen.LoadScene("QuickSort_InitialReadScene");


    }
}
