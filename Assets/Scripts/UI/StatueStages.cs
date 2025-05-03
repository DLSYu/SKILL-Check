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
    HO_5
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
        loadingScreen.LoadScene("ReadingMechanicNew");
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
            default:
                currentStage = statueStage.Not_Statue_Stage;
                break;

        }

    }


}
