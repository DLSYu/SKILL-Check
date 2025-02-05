using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StatueStages : MonoBehaviour, IPointerClickHandler
{
    private enum statueStage{
        HO_1,
        HO_2,
        HO_3,
        HO_4,
        HO_5
    }

    [SerializeField] statueStage currentStage;

    public void OnPointerClick(PointerEventData eventData){
        Debug.Log("Clicked");
        //SceneManager.LoadScene(currentStage.ToString());
        //Temporary placeholder
        SceneManager.LoadScene("PlatformerScene");
    }

    
}
