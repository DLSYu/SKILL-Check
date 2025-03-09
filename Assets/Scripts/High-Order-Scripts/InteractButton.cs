using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    // TO IMPLEMENT: GET LIST OF ALL INTERACTABLES CHECK IN UPDATES THE CLOSEST OBJECT TO INTERACT WITH
    GameObject currentInteractable;
    [SerializeField] private GameObject questionMark;

    void Update()
    {
        if (currentInteractable != null)
        {
            questionMark.SetActive(true);
        }
        else
        {
            questionMark.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log("Interactable near");
            currentInteractable = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IInteractable>() != null)
        {
            Debug.Log("Interactable left");
            currentInteractable = null;
        }
    }
    public void ClickInteractButton(){
        if (currentInteractable != null)
        {
            currentInteractable.GetComponent<IInteractable>().Interact();
        }
    }
}
