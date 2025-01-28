using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RelicSlot : MonoBehaviour
{
    public GameObject relic;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount > 0)
        {
            GameObject child = transform.GetChild(0).gameObject;
            if (child.GetComponent<RelicMovement>())
            {
                relic = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
