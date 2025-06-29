using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string stageToCheck = "";

    [SerializeField]
    private GameObject tutorial;

    private bool hasAlreadyRunTutorial = false;
    public void LoadData(GameData data)
    {
        data.stageCompletionDictionary.TryGetValue(stageToCheck, out hasAlreadyRunTutorial);

    }

    public void SaveData(GameData data)
    {

    }

    public void EnableTutorial()
    {

        if (!hasAlreadyRunTutorial)
            tutorial.SetActive(true);
    }
}
