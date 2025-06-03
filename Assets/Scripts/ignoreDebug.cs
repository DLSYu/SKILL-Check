using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ignoreDebug : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI text2;
    void Start()
    {
        text.text = Application.persistentDataPath;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadData(GameData data)
    {
        string thing = "";
        // TO DO: not sure what you need to load here for now
        foreach (object key in data.stageCompletionDictionary)
        {
            print(key);

        }

        text2.text = thing;


    }
    public void SaveData(GameData data)
    {
        // nothing
    }
}
