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
    private DoorInterface[] doorList;
    private int currentDoorIndex;
    [SerializeField]
    private TMPro.TextMeshProUGUI percentage;

    [SerializeField]
    private UIManagerTemplate uiManager;

    void Start()
    {
        currentDoorIndex = 0;
    }

    public DoorInterface GetCurrentDoor()
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
        percentage.text = "Score: ???";
    }
}
