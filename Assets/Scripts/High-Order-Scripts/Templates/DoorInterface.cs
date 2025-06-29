using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DoorInterface : MonoBehaviour, IInteractable
{
    [Header("Door Data")]
    [SerializeField] protected GameObject door;
    [SerializeField] protected float duration = 5f;
    [SerializeField] protected AudioClip doorSound;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected GameObject[] gems;
    [SerializeField] protected UIManagerTemplate uiManager;
    protected int activeGemCount;
    protected bool isDoorUnlocked = false;
    protected Vector3 startPosition;
    protected Vector3 movedPosition;
    protected float openElapsedTime = 0;
    protected bool triggerOpenOnce = false;

    // Start is called before the first frame update

    protected virtual void Start()
    {
        startPosition = door.transform.position;
        movedPosition = new Vector3(startPosition.x, startPosition.y + 5f, startPosition.z);
        activeGemCount = gems.Length;
    }
    protected virtual void Update()
    {
        if (isDoorUnlocked)
        {
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        }

        triggerDoorSound();
    }


    public virtual void Interact()
    {
        if (!isDoorUnlocked)
            uiManager.openTypingScreen();
    }

    protected void triggerDoorSound()
    {
        if (isDoorUnlocked && !triggerOpenOnce)
        {
            audioSource.PlayOneShot(doorSound);
            triggerOpenOnce = true;
        }
    }

    protected int countActiveGems()
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
    public bool checkIfDoorUnlocked()
    {
        return isDoorUnlocked;
    }

    public void unlockDoor()
    {
        isDoorUnlocked = true;
    }

    public Vector3 getDoorLocation()
    {
        return door.transform.position;
    }

    public List<Vector3> getActiveGemsLocations()
    {
        List<Vector3> activeGems = new List<Vector3>();
        foreach (GameObject gem in gems)
        {
            if (gem.activeSelf)
            {
                activeGems.Add(gem.transform.position);
            }
        }
        return activeGems;
    }

    public virtual String[] getDoorData()
    {
        String[] doorData = { "referenceTextHere", "keyWord" };
        return doorData;
    }
}
