using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Early : MonoBehaviour, IInteractable
{
    [Header("Story Data")]
    [TextArea(3, 10)]
    public string referenceText;
    [SerializeField] public string keyWord;

    [Header("Door Data")]
    [SerializeField] private GameObject door;
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager_Early uiManager;
    [SerializeField] private GameObject[] gems;

    private int activeGemCount;
    private bool isKeyWordUnlocked = false;
    private bool isDoorUnlocked = false;
    private Vector3 startPosition;
    private Vector3 movedPosition;
    private float openElapsedTime = 0;
    private bool triggerOpenOnce = false;

    void Start()
    {
        startPosition = door.transform.position;
        movedPosition = new Vector3(startPosition.x, startPosition.y + 5f, startPosition.z);
        activeGemCount = gems.Length;
    }

    void Update()
    {
        if (isDoorUnlocked)
        {
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        }

        TriggerDoorSound();
    }

    public void Interact()
    {
        uiManager.openTypingScreen();
    }

    private void TriggerDoorSound()
    {
        if (isDoorUnlocked && !triggerOpenOnce)
        {
            audioSource.PlayOneShot(doorSound);
            triggerOpenOnce = true;
        }
    }

    public bool CheckIfDoorUnlocked() => isDoorUnlocked;
    public void unlockDoor() => isDoorUnlocked = true;

    // public void CollectGem()
    // {
    //     activeGemCount--;
    //     Debug.Log("Gem collected. Remaining: " + activeGemCount);
    //     if (activeGemCount <= 0) EnableDoorInteraction();
    // }
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
    public Vector3 GetDoorLocation() => door.transform.position;
    public Vector3 getDoorLocation()
    {
        return door.transform.position;
    }
    public List<Vector3> getActiveGemsLocations()
    {
        List<Vector3> activeGems = new List<Vector3>();
        foreach (GameObject gem in gems)
        {
            if (gem.activeSelf) activeGems.Add(gem.transform.position);
        }
        return activeGems;
    }

    // public void EnableDoorInteraction()
    // {
    //     if (interactionCollider != null) interactionCollider.enabled = true;
    // }
}
