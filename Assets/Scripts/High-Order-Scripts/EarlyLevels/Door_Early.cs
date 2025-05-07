using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Early : DoorInterface
{

    protected override void Update()
    {
        if (isDoorUnlocked)
        {
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        }

        triggerDoorSound();
    }

    public override void Interact()
    {
        uiManager.openTypingScreen();
    }


}
