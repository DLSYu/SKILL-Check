using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool isPlayerNear = false;

    private Vector3 currentPosition;
    private Vector3 movedPosition;
    [SerializeField] private float duration = 5f;
    private float elapsedTime = 0;
    // Start is called before the first frame update

    void Start(){
        currentPosition = door.transform.position;
        movedPosition = new Vector3(currentPosition.x, currentPosition.y + 10f, currentPosition.z);
    }    
    void Update()
    {
        if (isPlayerNear){
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;
            door.transform.position = Vector3.Lerp(door.transform.position, movedPosition, percentageComplete);
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
}
