using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.IO;
using Unity.VisualScripting.Dependencies.Sqlite;

public class TextMeshInputHelper : MonoBehaviour
{
    public TextMeshProUGUI _tmp;
    public Canvas _canvas;
    public Camera _camera;
    public GameObject inputPanelPrefab;
    public string[] posList;
    private bool runOnce = false;
    public bool isDictionaryActive = false;

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
        if (IsTextLoaded())
        {

            _tmp.ForceMeshUpdate();

            TextAsset ta;


            string _title = GetTitle(_tmp.text);
            ta = Resources.Load<TextAsset>($"PartsOfSpeech/{_title}");

            string text = ta.text.ToString().Trim();

            posList = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            _tmp.ForceMeshUpdate();
            AttachButtonsToWords();
        }
    }

    public void Update()
    {



    }

    private bool IsTextLoaded()
    {
        if (string.IsNullOrEmpty(_tmp.text))
        {
            Debug.Log("TMP empty");
            return false;
        }
        Debug.Log(_tmp.text);
        return true;
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

            // Convert character positions to world space
            Vector3 bottomLeftWorld = _tmp.transform.TransformPoint(firstCharacter.bottomLeft);
            Vector3 topRightWorld = _tmp.transform.TransformPoint(lastCharacter.topRight);

            // Convert world space to local space of the parent panel
            Vector3 bottomLeftLocal = rectTransform.InverseTransformPoint(bottomLeftWorld);
            Vector3 topRightLocal = rectTransform.InverseTransformPoint(topRightWorld);

            Vector3 scale = rectTransform.lossyScale;
            float posX = (bottomLeftLocal.x + topRightLocal.x) / 2;
            float posY = (bottomLeftLocal.y + topRightLocal.y) / 2;
            float width = Mathf.Abs(topRightLocal.x - bottomLeftLocal.x);
            float height = Mathf.Abs(topRightLocal.y - bottomLeftLocal.y);

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

            panel.SetActive(false); // Initially deactivate the panel
        }
    }

    public void ActivateButtonsOnPage(int page)
    {
        if (isDictionaryActive == false) return;
        // This method is called when the page is changed
        // Activate the buttons on the current page
        TMP_TextInfo textInfo = _tmp.textInfo;

        // Get all attached input panels
        TextMeshInputPanel[] inputPanels = GetComponentsInChildren<TextMeshInputPanel>(true);

        foreach (TextMeshInputPanel panel in inputPanels)
        {
            int wordIndex = panel.GetWordIndex(); // assuming this returns the correct word index
            if (wordIndex < 0 || wordIndex >= textInfo.wordCount) continue;

            TMP_WordInfo wordInfo = textInfo.wordInfo[wordIndex];
            int charIndex = wordInfo.firstCharacterIndex;
            int charPage = textInfo.characterInfo[charIndex].pageNumber + 1;

            // Enable if it's on the current page
            bool isOnPage = (charPage == page);
            panel.gameObject.SetActive(isOnPage);
        }
    }

    public void DeactivateButtonsOnPage(int page)
    {
        // This method is called when the page is changed
        // Deactivate the buttons on the current page
        TMP_TextInfo textInfo = _tmp.textInfo;

        // Get all attached input panels
        TextMeshInputPanel[] inputPanels = GetComponentsInChildren<TextMeshInputPanel>(true);

        foreach (TextMeshInputPanel panel in inputPanels)
        {
            int wordIndex = panel.GetWordIndex(); // assuming this returns the correct word index
            if (wordIndex < 0 || wordIndex >= textInfo.wordCount) continue;

            TMP_WordInfo wordInfo = textInfo.wordInfo[wordIndex];
            int charIndex = wordInfo.firstCharacterIndex;
            int charPage = textInfo.characterInfo[charIndex].pageNumber + 1;

            // Disable if it's on the current page
            if (charPage == page)
                panel.gameObject.SetActive(false);
        }
    }

    private string GetTitle(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Convert escaped newlines ("\n" as text) into actual newlines
        input = input.Replace("\\n", "\n");

        // Normalize newlines
        input = input.Replace("\r\n", "\n");

        // Find the first occurrence of a double newline
        int index = input.IndexOf("\n\n");

        string title = index != -1 ? input.Substring(0, index) : input;

        // Remove invalid filename characters
        char[] invalidChars = Path.GetInvalidFileNameChars();
        title = new string(title.Where(c => !invalidChars.Contains(c)).ToArray());

        // Truncate title length
        const int maxLength = 50;
        if (title.Length > maxLength)
        {
            title = title.Substring(0, maxLength);
        }

        return title.Trim();
    }
}
