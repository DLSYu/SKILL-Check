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
    LO_5
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
        loadingScreen.LoadScene("BakunawaReadingMechanic");

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
            default:
                currentStage = bookStage.Not_Book_Stage;
                break;

        }

    }
}
