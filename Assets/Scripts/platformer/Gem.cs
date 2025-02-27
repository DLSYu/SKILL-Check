using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IInteractable
{
    [SerializeField] private DoorScript door;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioSource audioSource;

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
        audioSource.PlayOneShot(gemSound);
        
        door.condition = true;
        gameObject.SetActive(false);
        
    }
}
