using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private SWBSTSlot targetSlot;

    public void OnResetClicked()
    {
        if (targetSlot != null)
        {
            targetSlot.ResetSlot();
        }
    }
}
