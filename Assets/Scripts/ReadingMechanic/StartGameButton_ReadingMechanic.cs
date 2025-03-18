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
        loadingScreen.LoadScene("PlatformerScene");
    }
}
