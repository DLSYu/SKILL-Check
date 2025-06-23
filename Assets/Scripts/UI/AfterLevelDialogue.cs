using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class AfterLevelDialogue : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private List<string> messages;
    [SerializeField] private UnityEngine.UI.Image librarianImage;

    [SerializeField] private List<Sprite> librarianImageOrder;

    private int index;
    private bool hasHandledClick = false;

    public void OnEnable()
    {
        messageText.text = messages[0];
        librarianImage.sprite = librarianImageOrder[0];
        index = 1;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasHandledClick)
        {
            hasHandledClick = true;
            if (index < messages.Count)
            {

                messageText.text = messages[index];
                librarianImage.sprite = librarianImageOrder[index];
                index++;

            }
            else
            {
                this.gameObject.SetActive(false);
            }

            hasHandledClick = false;
        }
    }
}
