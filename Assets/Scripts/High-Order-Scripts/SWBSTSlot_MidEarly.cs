using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SWBSTSlot_MidEarly : MonoBehaviour
{
    public enum SlotType { Somebody, Wanted, But, So, Then }
    public SlotType slotType;
    [SerializeField] private TMP_InputField inputField;

    void Start()
    {
        // Initialize as non-interactable
        inputField.interactable = false;
    }

    public void SetInteractable(bool state)
    {
        inputField.interactable = state;

        if (!state)
        {
            inputField.text = "";
            if (inputField.placeholder != null)
                inputField.placeholder.gameObject.SetActive(true);
        }
    }
}
