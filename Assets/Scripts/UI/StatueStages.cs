using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public enum statueStage
{
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


}
