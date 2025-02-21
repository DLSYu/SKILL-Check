using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool condition = false;

    private bool unlocked = false;
    // Start is called before the first frame update

    void Start(){
        startPosition = door.transform.position;
        movedPosition = new Vector3(startPosition.x, startPosition.y + 5f, startPosition.z);
    }    
    void Update()
    {
        if (isPlayerNear && unlocked){
            // closeElapsedTime = 0;
            openElapsedTime += Time.deltaTime;
            float percentageComplete = openElapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
        } else{
            // openElapsedTime = 0;
            // closeElapsedTime += Time.deltaTime;
            // float percentageComplete = closeElapsedTime / duration;
            // door.transform.position = Vector3.Lerp(door.transform.position, startPosition, percentageComplete);
        }
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
        if(condition){
            unlocked = true;
        }
    }

}
