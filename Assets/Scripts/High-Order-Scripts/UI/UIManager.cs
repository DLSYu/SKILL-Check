using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject JoystickCanvas;
    [SerializeField] private GameObject TypingCanvas;
    [SerializeField] private GameObject GemCanvas;
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
    }

    public void exitTypingScreen(){
        TypingCanvas.SetActive(false);
        JoystickCanvas.SetActive(true);
    }

    public void openGemCanvas(String gemDescription)
    {
        GemCanvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = gemDescription;
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
