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
        if (StoryData.currentGameMode == "HighOrder")
            loadingScreen.LoadScene("PlatformerScene");
        else if (StoryData.currentGameMode == "LowOrder")
            loadingScreen.LoadScene("SortingScene");
    }
}
