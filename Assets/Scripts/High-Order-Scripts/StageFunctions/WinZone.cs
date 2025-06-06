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

    private float clearTime;
    private bool hasCleared = false;

    void Update()
    {
        if (!hasCleared)
            clearTime += Time.unscaledDeltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You Win!");


            uIAnimator.SetActive(true);
            audioSource.PlayOneShot(winSound);
            hasCleared = true;
            HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.SetClearTime(clearTime);

            DataPersistenceManager.instance.SaveGame();
        }
    }


    public void SaveData(GameData data)
    {
        if (hasCleared)
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

        HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics.SetDateTimeEnd(System.DateTime.Now.ToString());
        data.highOrderStageAnalyticsList.Add(HighOrderStageAnalyticsManager.instance.highOrderStageAnalytics);

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
        else if (spriteName == "statue_bakunawa")
            return "HO_3";
        else if (spriteName == "statue_adarna")
            return "HO_4";
        else if (spriteName == "statue_agila")
            return "HO_5";



        else
            return "";
    }

    void OnDestroy()
    {
        DataPersistenceManager.instance.SaveGame();
    }


}
