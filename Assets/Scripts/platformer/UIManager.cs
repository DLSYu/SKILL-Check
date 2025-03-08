using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject JoystickCanvas;
    [SerializeField] private GameObject TypingCanvas;
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

    public bool isTypingScreenOpen(){
        return TypingCanvas.activeSelf;
    }
    public bool isJoystickScreenOpen(){
        return JoystickCanvas.activeSelf;
    }
}
