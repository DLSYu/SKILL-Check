using UnityEngine;

public class RelicPlace : MonoBehaviour
{
    public GameObject correctRelic; // Assign the correct RelicPart in the Inspector
    public GameObject placedRelic; // The RelicPart currently placed here

    public bool IsCorrect => placedRelic == correctRelic;

    // Called when a RelicPart is placed here
    public void PlaceRelic(GameObject relic)
    {
        placedRelic = relic;
    }

    // Called when a RelicPart is removed
    public void RemoveRelic()
    {
        placedRelic = null;
    }
}