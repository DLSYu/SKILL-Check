using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class DoorManager_Early : MonoBehaviour
{
    [SerializeField] private Door_Early[] doorList;
    [SerializeField] private GameObject percentage;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private UIManager_Early uiManager;

    private int currentDoorIndex;

    void Start()
    {
        currentDoorIndex = 0;
    }

    public Door_Early GetCurrentDoor()
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

    public void ClearScorePanel()
    {
        percentage.SetActive(false);
        scorePanel.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}
