using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class IntroLobbyCutscene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private List<string> messages;
    [SerializeField] private UnityEngine.UI.Image librarianImage;

    [SerializeField] private List<Sprite> librarianImageOrder;

    private int index;
    private bool hasHandledClick = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!hasHandledClick)
        {
            hasHandledClick = true;
            if (index + 1 < messages.Count)
            {
                index++;
                messageText.text = messages[index];
                librarianImage.sprite = librarianImageOrder[index];

            }
            else
            {
                this.gameObject.SetActive(false);
            }

            hasHandledClick = false;
        }
    }
}
