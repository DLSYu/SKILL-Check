using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ONLY USED IN FIRST LEVEL
public class InteractButton_Tutorial : MonoBehaviour
{
    // TO IMPLEMENT: GET LIST OF ALL INTERACTABLES CHECK IN UPDATES THE CLOSEST OBJECT TO INTERACT WITH

    bool alreadyInitiated = false;
    GameObject currentInteractable;
    [SerializeField] private GameObject questionMark;
    [SerializeField] private GameObject interactButton;

    [SerializeField] private GameObject interactTutorial;

    void Update()
    {
        if (currentInteractable != null)
        {
            questionMark.SetActive(true);
            interactButton.SetActive(true);

            if (!alreadyInitiated && interactTutorial != null)
            {
                interactTutorial.SetActive(true);
                alreadyInitiated = true;
            }
        }
        else
        {
            questionMark.SetActive(false);
            interactButton.SetActive(false);
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
    public void ClickInteractButton()
    {
        if (currentInteractable != null)
        {
            currentInteractable.GetComponent<IInteractable>().Interact();
        }
    }


}
