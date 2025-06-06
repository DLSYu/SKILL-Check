using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Late Level UI needs text input handlng
public class UIManager_Late : UIManagerTemplate
{
    void Awake()
    {
        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics = new HighOrderStageTypeAnalytics(StageType.Late);
    }
    // Downcasted from doorInterface from doorManager to door_Early
    public override void openTypingScreen()
    {
        Time.timeScale = 0;
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

        Door_Late currentDoor = (Door_Late)doorManager.GetCurrentDoor();

        if (currentDoor.checkIfKeywordUnlocked())
        {
            keywordText.text = "Keyword: " + currentDoor.keyWord;
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
