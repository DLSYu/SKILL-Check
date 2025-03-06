using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInputManager : MonoBehaviour
{
    public static TextInputManager service = null;

    public Camera mainCamera;

    void Awake()
    {
        if (service != null)
        {
            Destroy(this.gameObject);
            return;
        }

        service = this;
    }


    public void SubscribeTMP(TextMeshProUGUI textMeshPro)
    {

    }
}
