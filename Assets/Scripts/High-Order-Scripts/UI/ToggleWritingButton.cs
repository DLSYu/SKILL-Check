using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum writingStyle{
        freeform,
        swbst
    }

public class ToggleWritingButton : MonoBehaviour
{
    [SerializeField]
    private GameObject freeFormPanel, swbstPanel;
    private writingStyle currentWritingStyle;

    public void ToggleWriting(){
        if (currentWritingStyle == writingStyle.freeform){
            currentWritingStyle = writingStyle.swbst;
            swbstPanel.SetActive(true);
            freeFormPanel.SetActive(false);
        }
        else{
            currentWritingStyle = writingStyle.freeform;
            freeFormPanel.SetActive(true);
            swbstPanel.SetActive(false);
        }
    }

    public writingStyle GetCurrentWritingStyle(){
        return currentWritingStyle;
    }

}
