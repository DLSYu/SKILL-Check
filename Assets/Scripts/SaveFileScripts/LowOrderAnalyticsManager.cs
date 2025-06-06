using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class LowOrderAnalyticsManager : MonoBehaviour
{

    public static LowOrderAnalyticsManager instance { get; private set; }

    // save file shenanigans
    public LowOrderStageAnalytics lowOrderStageAnalytics;
    public List<string> relicAnswers = new List<string>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one LowOrderAnalyticsManager Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        lowOrderStageAnalytics = new LowOrderStageAnalytics();

    }

}