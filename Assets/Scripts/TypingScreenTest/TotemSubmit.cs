using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

//using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TotemSubmit : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject percentage;
    [SerializeField]
    // will change to serialized list later
    private DoorManager doorObserver;
    [SerializeField]
    private TMP_InputField field1, field2, field3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData) {
        percentage.SetActive(true);
        float score = EvaluateScore();

        percentage.GetComponent<TextMeshProUGUI>().text = score.ToString();

        if (score >= 0.5f){
            this.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            doorObserver.GetCurrentDoor().unlocked = true;  
            doorObserver.SetNextDoor(); 
        }
        else{
            this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
    }

    private float EvaluateScore(){
        float score = 0.0f;

        // temporary score
        score = 0.4f;

        // Score evluation logic here
        string completeText = field1.text + " " + field2.text + " " + field3.text;
        string referenceText = doorObserver.GetCurrentDoor().referenceText;
        string keyWord = doorObserver.GetCurrentDoor().keyWord;

        // String logic here
        if (completeText.Contains(keyWord)){
            score += 0.1f;
            Debug.Log("KeyWord Bonus Points");
        }

        Debug.Log(completeText + " " + referenceText);

        

        return score;
    }
}
