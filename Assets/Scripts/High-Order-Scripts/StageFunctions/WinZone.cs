using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject uIAnimator;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip winSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You Win!");


            uIAnimator.SetActive(true);
            audioSource.PlayOneShot(winSound);
            DataPersistenceManager.instance.SaveGame();
        }
    }


    public void SaveData(GameData data)
    {
        bool value;
        data.stageCompletionDictionary.TryGetValue("HO_1", out value);
        // TO DO: pass the stage id 
        if (!value)
        {
            data.stageCompletionDictionary.Add("HO_1", true);
        }


    }

    public void LoadData(GameData data)
    {
        // do nothing
    }


}
