using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Early : DoorInterface
{
    [Header("Specific References")]
    [SerializeField] private UIManager_Early uiManagerEarly;

    protected override void Start()
    {
        startPosition = door.transform.position;
        movedPosition = new Vector3(startPosition.x, startPosition.y + 5f, startPosition.z);
        activeGemCount = gems.Length;
    }

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
        uiManagerEarly.openTypingScreen();
    }


}
