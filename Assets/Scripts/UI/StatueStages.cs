using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public enum statueStage
{
    Not_Statue_Stage,
    HO_1,
    HO_2,
    HO_3,
    HO_4,
    HO_5,
    HO_6
}

public class StatueStages : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private LoadingScreen loadingScreen;

    [SerializeField] statueStage currentStage;

    public void OnPointerClick(PointerEventData eventData)
    {
        //SceneManager.LoadScene(currentStage.ToString());
        //Temporary placeholder
        StoryData.SetCurrentHighOrderStage(currentStage);
        LoadReadingScene(currentStage);
    }

    public void SetLoadingScreen(LoadingScreen loadingScreen)
    {
        this.loadingScreen = loadingScreen;
    }

    public void SetCurrentStage(int number)
    {
        switch (number)
        {
            case 1:
                currentStage = statueStage.HO_1;
                break;
            case 2:
                currentStage = statueStage.HO_2;
                break;
            case 3:
                currentStage = statueStage.HO_3;
                break;
            case 4:
                currentStage = statueStage.HO_4;
                break;
            case 5:
                currentStage = statueStage.HO_5;
                break;
            case 6:
                currentStage = statueStage.HO_6;
                break;
            default:
                currentStage = statueStage.Not_Statue_Stage;
                break;

        }

    }

    private void LoadReadingScene(statueStage currentStage)
    {
        switch (currentStage)
        {
            case statueStage.HO_1:
                loadingScreen.LoadScene("HO1_ReadingScene");
                break;
            case statueStage.HO_2:
                loadingScreen.LoadScene("HO2_ReadingScene");
                break;
            case statueStage.HO_3:
                loadingScreen.LoadScene("HO3_ReadingScene");
                break;
            case statueStage.HO_4:
                loadingScreen.LoadScene("HO4_ReadingScene");
                break;
            case statueStage.HO_5:
                loadingScreen.LoadScene("HO5_ReadingScene");
                break;
            case statueStage.HO_6:
                loadingScreen.LoadScene("HO6_ReadingScene");
                break;
            default:
                loadingScreen.LoadScene("ReadingMechanicNew");
                break;

        }

    }

}
