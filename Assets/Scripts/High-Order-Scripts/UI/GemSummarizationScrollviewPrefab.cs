using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GemSummarizationScrollviewPrefab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gemType;
    [SerializeField] private TextMeshProUGUI gemDescription;
    [SerializeField] private Image gemImage;


    public void setGemType(string type)
    {
        gemType.text = type;
    }

    public void setGemDescription(string description)
    {
        gemDescription.text = description;
    }

    public void setGemImage(Sprite sprite)
    {
        gemImage.sprite = sprite;
    }
}
