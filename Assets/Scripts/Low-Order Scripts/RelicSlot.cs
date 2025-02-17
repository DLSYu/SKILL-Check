using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 *  Relic Slot is a GameObject that holds a Relic
 */
public class RelicSlot : MonoBehaviour
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
    // return true if a swap happened and false if no placedRelic was found and no swap happened.
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

        Debug.Log($"Swapping: {swapWith.name} and {name}");

        swapWith.placedRelic.GetComponent<RelicMovement>().OnSwap(this);
        swapWith.RemoveRelic();
        tempContatiner.GetComponent<RelicMovement>().OnSwap(swapWith);
        swapWith.PlaceRelic(tempContatiner);
    }
}
