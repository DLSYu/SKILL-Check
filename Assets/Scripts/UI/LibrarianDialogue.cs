using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LibrarianDialogue : MonoBehaviour, IPointerClickHandler
{
    private List<String> currentDialogueList = new List<string>();
    private List<Sprite> currentSpritesList = new List<Sprite>();
    [SerializeField] public List<String> openingDialogue = new List<string>();
    [SerializeField] private List<String> dialogueOption1 = new List<string>();
    [SerializeField] private List<String> dialogueOption2 = new List<string>();
    [SerializeField] private List<String> dialogueOption3 = new List<string>();
    [SerializeField] private List<String> dialogueOption4 = new List<string>();
    private List<List<String>> allDialogueOptions = new List<List<string>>();

    [SerializeField] private List<Sprite> openingDialogueSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> dialogueOptionSprite1 = new List<Sprite>();
    [SerializeField] private List<Sprite> dialogueOptionSprite2 = new List<Sprite>();
    [SerializeField] private List<Sprite> dialogueOptionSprite3 = new List<Sprite>();
    [SerializeField] private List<Sprite> dialogueOptionSprite4 = new List<Sprite>();

    private List<List<Sprite>> allDialogueSprites = new List<List<Sprite>>();

    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject messageObject;
    [SerializeField] private UnityEngine.UI.Image librarianImage;
    [SerializeField] private GameObject dialogueOptionsObject;


    private int index;
    private int dialogueChosen = -1;
    private bool hasHandledClick = false;
    private bool hasTappedOption = false;


    void Start()
    {
        allDialogueOptions.Add(dialogueOption1);
        allDialogueOptions.Add(dialogueOption2);
        allDialogueOptions.Add(dialogueOption3);
        allDialogueOptions.Add(dialogueOption4);

        allDialogueSprites.Add(dialogueOptionSprite1);
        allDialogueSprites.Add(dialogueOptionSprite2);
        allDialogueSprites.Add(dialogueOptionSprite3);
        allDialogueSprites.Add(dialogueOptionSprite4);

    }
    void OnEnable()
    {
        hasTappedOption = false;
        currentSpritesList = openingDialogueSprites;
        dialogueChosen = -1;


        // randomize the first dialogue option?
        string randomOpeningDialogue = openingDialogue[UnityEngine.Random.Range(0, openingDialogue.Count)];

        currentDialogueList = new List<String>
        {
            randomOpeningDialogue
        };
        message.text = randomOpeningDialogue;
        librarianImage.sprite = openingDialogueSprites[0];

        index = 1;



    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!dialogueOptionsObject.activeInHierarchy)
        {
            if (!hasHandledClick)
            {
                hasHandledClick = true;
                if (index < currentDialogueList.Count)
                {

                    message.text = currentDialogueList[index];
                    librarianImage.sprite = currentSpritesList[index];
                    messageObject.SetActive(true);
                    index++;

                }
                else if (dialogueChosen == -1)
                {
                    messageObject.SetActive(false);
                    // show dialogue tree if it hasn't been displayed
                    if (!dialogueOptionsObject.activeInHierarchy)
                        dialogueOptionsObject.SetActive(true);

                }
                else
                {
                    this.gameObject.SetActive(false);
                }

                hasHandledClick = false;
            }

        }
    }

    public void SetCurrentDialogueOption(int num)
    {
        if (!hasTappedOption)
        {
            hasTappedOption = true;
            dialogueOptionsObject.SetActive(false);
            dialogueChosen = num;

            currentDialogueList = allDialogueOptions[num];
            currentSpritesList = allDialogueSprites[num];

            message.text = currentDialogueList[0];
            librarianImage.sprite = currentSpritesList[0];
            messageObject.SetActive(true);

            index = 1;



        }


    }
}

