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
    private DoorScript currentDoor;
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
            currentDoor.unlocked = true;   
        }
        else{
            this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }
    }

    private float EvaluateScore(){
        float score = 0.0f;

        // Score evluation logic here
        String completeText = field1.text + " " + field2.text + " " + field3.text;
        Debug.Log(completeText);

        score = 0.5f;

        return score;
    }
}
