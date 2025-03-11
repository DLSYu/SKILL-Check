using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public static class DictionaryReader
{
    public static void ReadDictionary(string word)
    {
        // NOTE: This path may change when you make the production build.
        using (StreamReader r = new StreamReader(Application.dataPath + "/Resources/dictionary.json"))
        {
            string jsonData = r.ReadToEnd();
            List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(jsonData);
            foreach (Root root in myDeserializedClass)
            {
                if (root.englishWord == word)
                {
                    Debug.Log(root.definitions.noun);
                }
            }
        }
    }
}




