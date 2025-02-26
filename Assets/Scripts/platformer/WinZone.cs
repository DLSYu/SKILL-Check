using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField]
    private GameObject uIAnimator;
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            Debug.Log("You Win!");

            uIAnimator.SetActive(true);
        }
    }

    
}
