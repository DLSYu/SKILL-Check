using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Early level UI doesn't need to think about text input
public class UIManager_Early : UIManagerTemplate
{
    void Start()
    {
        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics = new HighOrderStageTypeAnalytics(StageType.Early);
    }
    public override void openTypingScreen()
    {
        Time.timeScale = 0;
        TypingCanvas.GetComponent<TypingPanel>().LoadCollectedGems();
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

    }

    public override void exitTypingScreen()
    {
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
        // if (isScorePanelCleanable) doorManager.ClearScorePanel();
        Time.timeScale = 1;
    }

    // public override void openGemCanvas(String gemDescription, String gemType, String gemName)
    // {
    //     Time.timeScale = 0;
    //     this.gemTMProDescription.text = gemDescription;
    //     this.gemTMProName.text = gemName;
    //     //Set Gem type
    //     GemCanvas.SetActive(true);
    //     JoystickCanvas.SetActive(false);
    // }

}
