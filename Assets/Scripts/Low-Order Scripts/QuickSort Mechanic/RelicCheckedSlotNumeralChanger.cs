using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RelicCheckedSlotNumeralChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numeral;
    public void ChangeNumeral(string toSet)
    {
        numeral.text = toSet;
    }


}
