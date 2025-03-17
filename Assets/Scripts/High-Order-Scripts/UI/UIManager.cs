using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject JoystickCanvas;
    [SerializeField] private GameObject TypingCanvas;
    [SerializeField] private GameObject GemCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI gemTMProDescription, gemTMProName;
    [SerializeField] private TMPro.TextMeshProUGUI keywordText;
    [SerializeField] private DoorManager doorManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openTypingScreen(){
        JoystickCanvas.SetActive(false);
        TypingCanvas.SetActive(true);

        if(doorManager.GetCurrentDoor().checkIfKeywordUnlocked()){
            keywordText.text = "Keyword: " + doorManager.GetCurrentDoor().keyWord;
        }
        else{
            keywordText.text = "Keyword: ???";
        }
    }

    public void exitTypingScreen(){
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    public void openGemCanvas(String gemDescription, String gemName)
    {
        this.gemTMProDescription.text = gemDescription;
        this.gemTMProName.text = gemName;
        GemCanvas.SetActive(true);
        JoystickCanvas.SetActive(false);
    }

    public void exitGemCanvas()
    {
        GemCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    // Status Checkers
    public bool isTypingScreenOpen(){
        return TypingCanvas.activeSelf;
    }
    public bool isJoystickScreenOpen(){
        return JoystickCanvas.activeSelf;
    }

}
