using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Debug.Log(this.gameObject.GetComponent<SpriteRenderer>().sprite.name);
        string stageName = DetermineStage(this.gameObject.GetComponent<SpriteRenderer>().sprite.name);
        data.stageCompletionDictionary.TryGetValue(stageName, out value);

        if (!value)
        {

            data.stageCompletionDictionary.Add(stageName, true);
        }


    }

    public void LoadData(GameData data)
    {
        // do nothing
    }

    private string DetermineStage(string spriteName)
    {
        if (spriteName == "statue_carabao")
            return "HO_1";
        else if (spriteName == "statue_tarsier")
            return "HO_2";
        else if (spriteName == "statue_adarna")
            return "HO_3";
        else if (spriteName == "statue_agila")
            return "HO_4";
        else if (spriteName == "statue_bakunawa")
            return "HO_5";
        else
            return "";
    }


}
