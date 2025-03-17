using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Story Data")]
    [TextArea(3, 10)]
    public String referenceText;
    [SerializeField]
    public String keyWord;

    [Header("Door Data")]
    [SerializeField] private GameObject door;
    [SerializeField] private float duration = 5f;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject[] gems;
    private int activeGemCount;
    private bool isKeyWordUnlocked = false;
    private bool isDoorUnlocked = false;
    private Vector3 startPosition;
    private Vector3 movedPosition;
    private float openElapsedTime = 0;
    private bool triggerOpenOnce = false;

    // Start is called before the first frame update

    void Start(){
        startPosition = door.transform.position;
        movedPosition = new Vector3(startPosition.x, startPosition.y + 5f, startPosition.z);
        activeGemCount = gems.Length;
    }    
    void Update()
    {
        if (isDoorUnlocked){
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        } 

        checkIfUnlockKeyword();

        triggerDoorSound();
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         isPlayerNear = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         isPlayerNear = false;
    //     }
    // }

   
    public void Interact()
    {
        uiManager.openTypingScreen();
    }

    private void triggerDoorSound(){
        if (isDoorUnlocked && !triggerOpenOnce){
            audioSource.PlayOneShot(doorSound);
            triggerOpenOnce = true;
        }
    }

    private void checkIfUnlockKeyword(){
        if(activeGemCount == 0){
            isKeyWordUnlocked = true;
        }
    }

    // public functions
    public bool checkIfDoorUnlocked(){
        return isDoorUnlocked;
    }

    public void unlockDoor(){
        isDoorUnlocked = true;
    }

    public void collectGem(){
        activeGemCount--;
        Debug.Log("Gem collected. Remaining: " + activeGemCount);
    }
    public bool checkIfKeywordUnlocked(){
        return isKeyWordUnlocked;
    }
}
