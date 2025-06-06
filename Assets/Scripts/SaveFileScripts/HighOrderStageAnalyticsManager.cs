using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class HighOrderStageAnalyticsManager : MonoBehaviour
{

    public static HighOrderStageAnalyticsManager instance { get; private set; }

    // save file shenanigans
    public HighOrderStageAnalytics highOrderStageAnalytics;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one HighOrderStageAnalyticsManager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        highOrderStageAnalytics = new HighOrderStageAnalytics();

    }


}