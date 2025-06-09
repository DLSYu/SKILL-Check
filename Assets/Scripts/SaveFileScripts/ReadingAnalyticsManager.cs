using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ReadingAnalyticsManager : MonoBehaviour, IDataPersistence
{

    public static ReadingAnalyticsManager instance { get; private set; }

    // save file shenanigans
    public ReadingAnalytics readingAnalytics;

    public bool hasTutorialPlayed;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one ReadingAnalyticsManager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        readingAnalytics = new ReadingAnalytics();

        readingAnalytics.SetStartingStats(System.DateTime.Now.ToString(), SceneManager.GetActiveScene().name);


    }

    void Update()
    {
        if (!readingAnalytics.GetContinuedToGame())
            readingAnalytics.AddTimeTakenReading(Time.deltaTime);
    }

    public void LoadData(GameData data)
    {
        // nothing
    }

    public void SaveData(GameData data)
    {
        readingAnalytics.SetDateTimeEnd(System.DateTime.Now.ToString());
        data.readingAnalyticsList.Add(readingAnalytics);

        if (hasTutorialPlayed)
        {
            bool value;
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("ReadingMechanicTutorial", out value);

            if (!value)
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("ReadingMechanicTutorial", true);
        }
    }

    void OnDestroy()
    {
        DataPersistenceManager.instance.SaveGame();
    }
}