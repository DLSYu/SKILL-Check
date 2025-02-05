using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BookStages : MonoBehaviour, IPointerClickHandler
{
    private enum bookStage{
        LO_1,
        LO_2,
        LO_3,
        LO_4,
        LO_5
    }

    [SerializeField] bookStage currentStage;

    public void OnPointerClick(PointerEventData eventData){
        //SceneManager.LoadScene(currentStage.ToString());
        //Temporary placeholder
        SceneManager.LoadScene("SortingScene");

    }
}
