using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MidEarly : UIManagerTemplate
{

    void Awake()
    {
        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.highOrderStageTypeAnalytics = new HighOrderStageTypeAnalytics(StageType.Mid);
    }
    public override void openTypingScreen()
    {
        Time.timeScale = 0;
        TypingCanvas.GetComponent<TypingPanel>().LoadCollectedGems();
        TypingCanvas.GetComponent<TypingPanel>().LoadTypingPanels();
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

        // FindObjectOfType<TypingPanel_MidEarly>().ShowStoryAndGems();

        Door_MidEarly currentDoor = (Door_MidEarly)doorManager.GetCurrentDoor();
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

    // public void RefreshTypingPanel()
    // {
    //     var typingPanel = TypingCanvas.GetComponent<TypingPanel_MidEarly>();
    //     if (typingPanel != null)
    //     {
    //         typingPanel.ShowStoryAndGems();
    //     }
    // }
}
