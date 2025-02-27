using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField]
    private GameObject uIAnimator;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip winSound;
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            Debug.Log("You Win!");

            uIAnimator.SetActive(true);
            audioSource.PlayOneShot(winSound);
        }
    }

    
}
