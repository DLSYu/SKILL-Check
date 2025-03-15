using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class TextMeshInputHelper : MonoBehaviour
{
    public TextMeshProUGUI _tmp;
    public Canvas _canvas;
    public Camera _camera;
    public GameObject inputPanelPrefab;
    public string[] posList;

    public void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        if (_tmp == null)
        {
            Debug.LogError("Required a TextMeshProUGUI object from " + this.name);
        }

        if (_camera == null)
        {
            Debug.LogError($"{this.name} requires a camera!");
        }
    }

    public void Start()
    {
        _tmp.ForceMeshUpdate();


        TextAsset ta = Resources.Load<TextAsset>($"PartsOfSpeech/{_tmp.text}");
        string text = ta.text.ToString().Trim();

        posList = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        _tmp.ForceMeshUpdate();
        AttachButtonsToWords();
    }

    public void AttachButtonsToWords()
    {
        RectTransform rectTransform = (RectTransform)this.transform;
        TMP_TextInfo textInfo = _tmp.textInfo;
        for (int i = 0; i < textInfo.wordCount; i++)
        {
            TMP_WordInfo wordInfo = textInfo.wordInfo[i];
            TMP_CharacterInfo firstCharacter = textInfo.characterInfo[wordInfo.firstCharacterIndex];
            TMP_CharacterInfo lastCharacter = textInfo.characterInfo[wordInfo.lastCharacterIndex];

            Vector3 bottomLeft = firstCharacter.bottomLeft;
            Vector3 topRight = lastCharacter.topRight;

            float posX = (bottomLeft.x + topRight.x) / 2;
            float posY = (bottomLeft.y + topRight.y) / 2;
            float width = Mathf.Abs(topRight.x - bottomLeft.x);
            float height = Mathf.Abs(topRight.y - bottomLeft.y);

            GameObject panel = Instantiate(inputPanelPrefab, this.transform);
            TextMeshInputPanel ip = panel.GetComponent<TextMeshInputPanel>();
            ip.rt.anchorMin = rectTransform.anchorMin;
            ip.rt.anchorMax = rectTransform.anchorMax;
            ip.rt.anchoredPosition = new Vector3(posX, posY, ip.rt.position.z);
            ip.rt.sizeDelta = new Vector2(width, height);
            ip._tmp = _tmp;
            ip.SetWordIndex(i);

            // Find the POS
            ip.POS = posList[i];

            POS value;
            Enum.TryParse(ip.POS, out value);
            if (value.ToString() == ip.POS.ToString())
            {
                ip.dictionaryDefinition = DictionaryReader.ReadDictionary(_tmp.textInfo.wordInfo[i].GetWord().ToLower(), value);
            }
            else
            {
                Destroy(ip.gameObject);
                continue;
            }

            if (ip.dictionaryDefinition == null || !ip.dictionaryDefinition.exists)
            {
                Destroy(ip.gameObject);
            }
        }
    }
}
