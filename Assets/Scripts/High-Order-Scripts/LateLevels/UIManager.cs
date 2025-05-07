using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine;

// Late Level UI needs text input handlng
public class UIManager : UIManagerTemplate
{

    public override void openTypingScreen()
    {
        Time.timeScale = 0;
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

        if (doorManager.GetCurrentDoor().checkIfKeywordUnlocked())
        {
            keywordText.text = "Keyword: " + doorManager.GetCurrentDoor().keyWord;
        }
        else
        {
            keywordText.text = "Keyword: ???";
        }
    }

    public override void exitTypingScreen()
    {
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);


        if (isScorePanelCleanable)
        {
            doorManager.clearScorePanel();
            gateSubmit.clearAllfields();
        }
        Time.timeScale = 1;
    }

}
