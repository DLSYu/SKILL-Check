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
        }

        if (currentDoorIndex == doorList.Length - 1)
        {
            Debug.Log("All doors unlocked");
        }
    }
}
