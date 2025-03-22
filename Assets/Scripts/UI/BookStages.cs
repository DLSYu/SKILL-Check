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
        loadingScreen.LoadScene("ReadingMechanicNew");

    }
}
