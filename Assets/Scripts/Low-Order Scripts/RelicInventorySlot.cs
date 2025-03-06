using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Relic Inventory Slot is a Relic Slot in the Player's inventory
 */
public class RelicInventorySlot : RelicSlot
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount == 1)
        {
            GameObject child = transform.GetChild(0).gameObject;
            if (child.GetComponent<RelicMovement>())
            {
                placedRelic = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
