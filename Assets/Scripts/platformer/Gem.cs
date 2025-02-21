using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorScript door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Gem collected");
        gameObject.SetActive(false);
        door.condition = true;
    }
}
