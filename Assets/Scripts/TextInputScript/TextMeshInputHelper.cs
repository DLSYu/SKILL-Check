using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using System.Globalization;
using System;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

public class TextMeshInputHelper : MonoBehaviour
{
    public TextMeshProUGUI _tmp;
    public Canvas _canvas;
    public Camera _camera;
    public GameObject inputPanelPrefab;

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
            InputPanel ip = panel.GetComponent<InputPanel>();
            ip.rt.anchorMin = rectTransform.anchorMin;
            ip.rt.anchorMax = rectTransform.anchorMax;
            ip.rt.anchoredPosition = new Vector3(posX, posY, ip.rt.position.z);
            ip.rt.sizeDelta = new Vector2(width, height);
        }
    }
}
