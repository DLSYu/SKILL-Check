using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject door;
    private bool isPlayerNear = false;

    private Vector3 startPosition;
    private Vector3 movedPosition;
    [SerializeField] private float duration = 5f;
    private float openElapsedTime = 0;
    private float closeElapsedTime = 0;
    public bool unlocked = false;
    private bool triggerOpenOnce = false;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIManager uiManager;
    // Start is called before the first frame update

    void Start(){
        startPosition = door.transform.position;
        movedPosition = new Vector3(startPosition.x, startPosition.y + 5f, startPosition.z);
    }    
    void Update()
    {
        //move door action
        if (isPlayerNear && unlocked){
            // closeElapsedTime = 0;
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        } 
        // else{
        //     openElapsedTime = 0;
        //     closeElapsedTime += Time.deltaTime;
        //     float percentageComplete = closeElapsedTime / duration;
        //     door.transform.position = Vector3.Lerp(door.transform.position, startPosition, percentageComplete);
        // }
        triggerDoorSound();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerNear = false;
        }
    }

   
    public void Interact()
    {
        uiManager.openTypingScreen();
        
        // unlocked = true;
    }

    private void triggerDoorSound(){
        if (isPlayerNear && unlocked && !triggerOpenOnce){
            audioSource.PlayOneShot(doorSound);
            triggerOpenOnce = true;
        }
    }

}
