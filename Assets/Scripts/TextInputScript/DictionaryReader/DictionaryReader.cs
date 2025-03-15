using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public static class DictionaryReader
{
    public static InTextDefinition ReadDictionary(string word, POS pos)
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
                    InTextDefinition itd = new("", "");
                    switch (pos)
                    {
                        case POS.PROPN:
                        case POS.NOUN:
                            itd.definition = root.definitions.noun;
                            itd.example = root.examplesSentence.noun;
                            break;
                        case POS.VERB:
                            itd.definition = root.definitions.verb;
                            itd.example = root.examplesSentence.verb;
                            break;
                        case POS.ADJ:
                            itd.definition = root.definitions.adjective;
                            itd.example = root.examplesSentence.adjective;
                            break;
                        case POS.ADV:
                            itd.definition = root.definitions.adverb;
                            itd.example = root.examplesSentence.adverb;
                            break;
                        case POS.CONJ:
                            itd.definition = root.definitions.conjunction;
                            itd.example = root.examplesSentence.conjunction;
                            break;
                        case POS.INTJ:
                            itd.definition = root.definitions.interjection;
                            itd.example = root.examplesSentence.interjection;
                            break;
                        case POS.PRON:
                            itd.definition = root.definitions.pronoun;
                            itd.example = root.examplesSentence.pronoun;
                            break;
                        default:
                            itd.exists = false;
                            break;
                    }
                    return itd;
                }
            }
        }
        return null;
    }
}




