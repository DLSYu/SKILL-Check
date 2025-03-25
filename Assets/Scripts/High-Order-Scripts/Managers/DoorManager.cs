using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    private Door[] doorList;
    private int currentDoorIndex;
    [SerializeField]
    private GameObject percentage, scorePanel;
    [SerializeField]
    private UIManager uiManager;

    void Start()
    {
        currentDoorIndex = 0;
    }

    public Door GetCurrentDoor()
    {
        return doorList[currentDoorIndex];
    }

    public void SetNextDoor()
    {
        if (currentDoorIndex < doorList.Length - 1)
        {
            currentDoorIndex++;
            uiManager.isScorePanelCleanable = true;
        }

        if (currentDoorIndex == doorList.Length - 1)
        {
            Debug.Log("All doors unlocked");
        }
    }

    public void clearScorePanel()
    {
        percentage.SetActive(false);
        scorePanel.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
