using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputfieldLine : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private TMP_InputField first_field;
    [SerializeField] private TMP_InputField previous_field;
    [SerializeField] private TMP_InputField next_field;

    int max_characters = 48;
    private bool handling_backspace = false;
    private TMP_InputField current_field;
    void Start()
    {

        current_field = this.GetComponent<TMP_InputField>();
    }

     public void HandleTextOverflow()
    {
       
        if (!Input.GetKeyDown(KeyCode.Backspace))
        {
            string input = current_field.text.Trim((char)8203);
            if (input.Length > max_characters && next_field != null)
            {
                // Find the last space before max_characters limit to split at a word boundary
                int splitIndex = input.LastIndexOf(' ', max_characters);
                if (splitIndex == -1) splitIndex = max_characters; // If no space found, split at max length

                // Move overflow text to the next field
                string overflowText = input.Substring(splitIndex).TrimStart();
                next_field.text += overflowText;

                // Trim current field's text up to the split point
                current_field.text = input.Substring(0, splitIndex);

                // Move focus to the next input field
                next_field.Select();
                next_field.caretPosition = overflowText.Length;
                next_field.MoveToEndOfLine(false, false);
                
            }
        }
        
    }

    public void HandleBackspace()
    {
        Debug.Log("caret pos:" + current_field.caretPosition);
        // Check if backspace is pressed and caret is at the start
        if (Input.GetKeyDown(KeyCode.Backspace) && current_field.caretPosition - 1 == -1 && previous_field != null && !handling_backspace)
        {
            Debug.Log("backspace");
            handling_backspace = true;

            // if no letter after caret
            if (string.IsNullOrEmpty(current_field.text.Trim((char)8203)))
            {
                previous_field.Select();
                previous_field.caretPosition=previous_field.text.Length;
            }
            // there's letters after caret
            else {
                // Find the last word in the previous field
                int lastSpaceIndex = previous_field.text.LastIndexOf(' ');
                string last_part = (lastSpaceIndex != -1) 
                    ? previous_field.text.Substring(lastSpaceIndex).TrimStart()
                    : previous_field.text; // If no space, take entire text

                if (previous_field.text.Trim((char)8203).Length + last_part.Length > max_characters )
                {
                    // Remove the last word from the previous field
                    previous_field.text = (lastSpaceIndex != -1) 
                    ? previous_field.text.Substring(0, lastSpaceIndex) 
                    : "";
                    current_field.text = last_part + current_field.text;
                    current_field.caretPosition = current_field.text.Length;
                    current_field.MoveToEndOfLine(false, false);
                }

                else {
                    previous_field.Select();
                    previous_field.text = previous_field.text.Substring(0, previous_field.text.Length-1);
                    previous_field.MoveToEndOfLine(false, false); //= previous_field.text.Length;
                }

            }
            
           

           

             
            
        }
        else if (Input.GetKeyDown(KeyCode.Backspace) && next_field != null && next_field.text.Trim((char)8203).Length != 0)
            {
                int first_space_index = next_field.text.IndexOf(' ');
                
                string first_space_string = (first_space_index != -1) 
                        ? next_field.text.Substring(0, first_space_index)
                        : next_field.text;

                Debug.Log(first_space_string);
                if (current_field.text.Trim((char)8203).Length + first_space_string.Length <= max_characters)
                    {
                        if (first_space_index == -1)
                           next_field.text = "";
                        else
                            next_field.text = next_field.text.Substring(first_space_index, next_field.text.Length);
            
                        current_field.text = current_field.text + first_space_string;
                        current_field.Select();
                        current_field.caretPosition = current_field.text.Length;
                    }
            }
        handling_backspace = false;

      
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (previous_field != null && current_field.text.Trim((char)8203).Length == 0)
        {
            if (first_field.text.Trim((char)8203).Length == 0 && previous_field.text.Trim((char)8203).Length > 0)
                {
                    previous_field.Select();
                    previous_field.caretPosition = previous_field.text.Length;
                }
            else {
                first_field.Select();
                first_field.caretPosition = first_field.text.Length;
            }
            
        }
        else if (previous_field == null)
         {
                first_field.Select();
                first_field.caretPosition = first_field.text.Length;
            }
    
        
    }


    // Update is called once per frame
    void Update()
    {
       
    }

}
