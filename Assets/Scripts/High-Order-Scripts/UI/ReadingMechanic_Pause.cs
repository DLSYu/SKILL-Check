using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingMechanic_Pause : MonoBehaviour
{

    [SerializeField] private GameObject MenuAnimationHandler;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private LoadingScreen loadingScreen;
    private void Awake()
    {

        Screen.SetResolution(2000, 1200, true);
    }


    public void openMenu()
    {
        //pause game
        //deactivate control canvas
        //open menu to go back to main menu

        Time.timeScale = 0;
        MenuAnimationHandler.SetActive(true);
        // Menu Canvas set active handled by UIAnimator


    }

    public void exitMenu()
    {
        //resume game
        //activate control canvas
        //close menu
        Time.timeScale = 1;

        MenuAnimationHandler.SetActive(false);
        MenuCanvas.SetActive(false);
    }
    public void quitStage()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        loadingScreen.LoadScene("Lobby");
    }
}
