using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Door_Late : DoorInterface
{
    [Header("Late Level Data")]
    [TextArea(3, 10)]
    public string referenceText;
    [SerializeField]
    public string keyWord;
    private bool isKeyWordUnlocked = false;

    protected override void Update()
    {
        if (isDoorUnlocked)
        {
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        }

        checkIfUnlockKeyword();
        triggerDoorSound();
    }


    private void checkIfUnlockKeyword()
    {
        if (countActiveGems() == 0)
        {
            isKeyWordUnlocked = true;
        }
    }

    // public functions


    public bool checkIfKeywordUnlocked()
    {
        return isKeyWordUnlocked;
    }

    public override String[] getDoorData()
    {
        String[] doorData = new String[2];
        doorData[0] = referenceText;
        doorData[1] = keyWord;
        return doorData;
    }

}
