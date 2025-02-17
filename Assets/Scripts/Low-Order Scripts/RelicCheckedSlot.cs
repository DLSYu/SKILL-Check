using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Relic Checked Slot is a Relic Slot that gets checked for the correct sequence.
 */
public class RelicCheckedSlot : RelicSlot
{
    public GameObject correctRelic; // Assign the correct RelicPart in the Inspector
    public bool IsCorrect => placedRelic == correctRelic;
}
