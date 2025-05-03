using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DateMessagePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;


    public void setDateText(string date)
    {
        dateText.text = date;
    }
}
