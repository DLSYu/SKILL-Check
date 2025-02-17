using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SortingGameManager : MonoBehaviour
{
    public static SortingGameManager Instance;
    public RelicCheckedSlot[] relicPlaces; // Array of RelicPlace objects

    private float timer = 0f;

    void Awake() => Instance = this;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void CheckCompletion()
    {
        foreach (var place in relicPlaces)
        {
            if (!place.IsCorrect)
            {
                Debug.Log($"Incorrect relic in place: {place.name}");
                return;
            }
        }

        // All relics are correct
        StartCoroutine(LoadEndSceneAfterDelay(2f)); // 2-second delay
    }

    IEnumerator LoadEndSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Sequence_End");
    }
}