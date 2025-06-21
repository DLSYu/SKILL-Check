using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptIfTutorialPlayedHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject promptHandler;
    bool hasTutorialPlayed = false;

    void Start()
    {

        if (hasTutorialPlayed)
        {
            promptHandler.SetActive(true);
        }
    }
    public void LoadData(GameData data)
    {
        data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("ReadingMechanicTutorial", out hasTutorialPlayed);
    }

    public void SaveData(GameData data)
    {

    }
}
