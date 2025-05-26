using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton_ReadingMechanic : MonoBehaviour
{
    [SerializeField]
    private LoadingScreen loadingScreen;
    public void startGameScene()
    {
        if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_1)
            loadingScreen.LoadScene("PlatformerSceneEarly");
        else if (StoryData.currentGameMode == "HighOrder" && StoryData.currentStatueStage == statueStage.HO_2)
            loadingScreen.LoadScene("PlatformerSceneLate");
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
    }
}
