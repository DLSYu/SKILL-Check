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
    public String referenceText;
    [SerializeField]
    public String keyWord;
    private bool isKeyWordUnlocked = false;
    // Start is called before the first frame update
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

    private int countActiveGems()
    {
        activeGemCount = 0;
        foreach (GameObject gem in gems)
        {
            if (gem.activeSelf)
            {
                activeGemCount++;
            }
        }
        return activeGemCount;
    }

    // public functions


    public bool checkIfKeywordUnlocked()
    {
        return isKeyWordUnlocked;
    }

}
