using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class GameData
{
    public SerializedDictionary<string, bool> stageCompletionDictionary;
    // Start is called before the first frame update
    public GameData()
    {
        this.stageCompletionDictionary = new SerializedDictionary<string, bool>();
    }
}
