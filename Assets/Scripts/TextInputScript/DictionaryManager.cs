using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class DictionaryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dictionaryPanel;
    [SerializeField]
    private TextMeshProUGUI clickedWord;
    [SerializeField]
    private TextMeshProUGUI definitionText;

    [SerializeField]
    private GameObject blackBackground;



    public void ShowDictionaryPanel(string text, InTextDefinition definition)
    {
        dictionaryPanel.SetActive(true);
        blackBackground.SetActive(true);
        this.clickedWord.text = text.FirstCharacterToUpper();
        definitionText.text = definition.ToString();

        ReadingAnalyticsManager.instance.readingAnalytics.AddNewDictionaryWord(this.clickedWord.text);
    }

    public void HideDictionaryPanel()
    {
        dictionaryPanel.SetActive(false);
        blackBackground.SetActive(false);
    }


}
