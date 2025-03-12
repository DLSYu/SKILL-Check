using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
 *  Relic Slot is a GameObject that holds a Relic
 */
public class RelicSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject placedRelic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when a RelicPart is placed here
    // return true if a swap happened and false if no placedRelic was found and no swap happened.s
    public bool PlaceRelic(GameObject relicToPlace)
    {
        bool toReturn = true;

        //check if swap is necessary
        if(placedRelic != null)
        {
            // Takes care of setting the originalParent, newParent, transform.parent, and position.
            SwapRelics(relicToPlace.GetComponent<RelicMovement>().originalParent);
            toReturn = false;
        }
        
        placedRelic = relicToPlace;

        return toReturn;
    }

    // Called when a RelicPart is removed
    public void RemoveRelic()
    {
        placedRelic = null;

    }

    public void SwapRelics (RelicSlot swapWith)
    {
        GameObject tempContatiner = placedRelic;

        //Debug.Log($"Swapping: {swapWith.name} and {name}");

        swapWith.placedRelic.GetComponent<RelicMovement>().OnSwap(this);
        swapWith.RemoveRelic();
        tempContatiner.GetComponent<RelicMovement>().OnSwap(swapWith);
        swapWith.PlaceRelic(tempContatiner);
    }

    // Detect tap/click on the RelicSlot
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the tap/click is valid (e.g., not during dragging)
        if (!IsDraggingRelic())
        {
            Debug.Log($"Tapped on RelicSlot: {name}");
            LoadReadingScene();
        }
    }

    // Check if a relic is currently being dragged
    private bool IsDraggingRelic()
    {
        // Check if any relic is being dragged
        RelicMovement[] relics = FindObjectsOfType<RelicMovement>();
        foreach (var relic in relics)
        {
            if (relic.IsDragging())
            {
                return true;
            }
        }
        return false;
    }

    // Load the Reading_Scene
    private void LoadReadingScene()
    {
        SceneManager.LoadScene("Reading_Scene");
    }
}
