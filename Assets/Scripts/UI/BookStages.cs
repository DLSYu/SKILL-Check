using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum bookStage
{
    Not_Book_Stage,
    LO_1,
    LO_2,
    LO_3,
    LO_4,
    LO_5,
    LO_6
}

public class BookStages : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private LoadingScreen loadingScreen;


    [SerializeField] bookStage currentStage;

    public void OnPointerClick(PointerEventData eventData)
    {
        //SceneManager.LoadScene(currentStage.ToString());
        //Temporary placeholder
        StoryData.SetCurrentLowOrderStage(currentStage);
        LoadReadingScene(currentStage);
    }

    public void setLoadingScreen(LoadingScreen loadingScreen)
    {
        this.loadingScreen = loadingScreen;
    }

    public void SetCurrentStage(int number)
    {
        switch (number)
        {
            case 1:
                currentStage = bookStage.LO_1;
                break;
            case 2:
                currentStage = bookStage.LO_2;
                break;
            case 3:
                currentStage = bookStage.LO_3;
                break;
            case 4:
                currentStage = bookStage.LO_4;
                break;
            case 5:
                currentStage = bookStage.LO_5;
                break;
            case 6:
                currentStage = bookStage.LO_6;
                break;
            default:
                currentStage = bookStage.Not_Book_Stage;
                break;

        }

    }

    private void LoadReadingScene(bookStage currentStage)
    {
        switch (currentStage)
        {
            case bookStage.LO_1:
                loadingScreen.LoadScene("LO1_ReadingScene");
                break;
            case bookStage.LO_2:
                loadingScreen.LoadScene("LO2_ReadingScene");
                break;
            case bookStage.LO_3:
                loadingScreen.LoadScene("LO3_ReadingScene");
                break;
            case bookStage.LO_4:
                loadingScreen.LoadScene("LO4_ReadingScene");
                break;
            case bookStage.LO_5:
                loadingScreen.LoadScene("LO5_ReadingScene");
                break;
            case bookStage.LO_6:
                loadingScreen.LoadScene("LO6_ReadingScene");
                break;
            default:
                loadingScreen.LoadScene("ReadingMechanicNew");
                break;

        }

    }
}
