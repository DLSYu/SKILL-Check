using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  RelicCheckedSlot now functions as the drop zone on the dragon that checks relics in a defined sequence.
 */
public class RelicCheckedSlot : RelicSlot
{
    [Tooltip("Assign the relic parts in the correct order (e.g., first element = Relic Part 3, etc.).")]
    public List<GameObject> correctSequence;  // Correct order defined in the Inspector

    // Internal list to track which relics have been placed so far
    private List<GameObject> placedRelics = new List<GameObject>();

    // Returns true when all relics have been correctly placed
    public bool IsSequenceComplete => placedRelics.Count == correctSequence.Count;

    // Call this method when a relic is dropped on the dragon.
    // It checks if the relic is the expected one in the sequence.
    public bool TryPlaceRelic(GameObject relic)
    {
        int currentIndex = placedRelics.Count;
        if (currentIndex < correctSequence.Count)
        {
            if (relic == correctSequence[currentIndex])
            {
                placedRelics.Add(relic);
                Debug.Log($"Correct relic {relic.name} placed at position {currentIndex + 1}");
                return true;
            }
            else
            {
                Debug.Log($"Incorrect relic {relic.name} for position {currentIndex + 1}. Expected: {correctSequence[currentIndex].name}");
                return false;
            }
        }
        return false;
    }

    // Method to reset the sequence if you want to allow reattempts.
    public void ResetSequence()
    {
        placedRelics.Clear();
    }
}
