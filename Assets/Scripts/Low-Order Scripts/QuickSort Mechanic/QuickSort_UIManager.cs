using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort_UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuAnimationHandler;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private LoadingScreen loadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openMenu()
    {
        //pause game
        //open menu to go back to main menu

        Time.timeScale = 0;
        MenuAnimationHandler.SetActive(true);
        // Menu Canvas set active handled by UIAnimator


    }

    public void exitMenu()
    {
        //resume game
        //close menu
        Time.timeScale = 1;

        MenuAnimationHandler.SetActive(false);
        MenuCanvas.SetActive(false);
    }

    public void quitStage()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        Destroy(QuickSortSortingGameManager.Instance.gameObject);
        loadingScreen.LoadScene("Lobby");
    }
}
