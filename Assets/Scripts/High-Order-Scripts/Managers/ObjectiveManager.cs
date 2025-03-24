using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] private DoorManager doorManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject objectivePanel;
    [SerializeField] private GameObject optionalPanel;
    [SerializeField] private UnityEngine.UI.Image objectiveButtonImage;
    [SerializeField] private UnityEngine.UI.Image optionalButtonImage;
    // [SerializeField] private TMPro.TextMeshProUGUI objectiveText;
    private Vector3 playerPosition;
    private bool isPathFinding = false;


    void Update()
    {

    }
    public void findDoor()
    {
        if (isPathFinding) { return; }
        // get locations
        playerPosition = player.transform.position;
        Vector3 doorPosition = doorManager.GetCurrentDoor().getDoorLocation();
        // set arrow active and put on player location
        StartCoroutine(startArrowPathfinding(doorPosition));

    }

    public void findGems()
    {
        if (isPathFinding) { return; }
        // get closest gem location to player
        playerPosition = player.transform.position;
        List<Vector3> gemLocations = doorManager.GetCurrentDoor().getActiveGemsLocations();

        if (gemLocations.Count == 0)
        {
            Debug.Log("No gems to find");
            return;
        }
        else
        {
            Vector3 closestGem = gemLocations[0];
            foreach (Vector3 gemLocation in gemLocations)
            {
                if (Vector3.Distance(playerPosition, gemLocation) < Vector3.Distance(playerPosition, closestGem))
                {
                    closestGem = gemLocation;
                }
            }
            StartCoroutine(startArrowPathfinding(closestGem));
        }

    }

    public void toggleObjectivePanel()
    {
        if (objectivePanel.activeSelf)
        {
            objectivePanel.SetActive(false);
            objectiveButtonImage.rectTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            objectivePanel.SetActive(true);
            objectiveButtonImage.rectTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void toggleOptionalPanel()
    {
        if (optionalPanel.activeSelf)
        {
            optionalPanel.SetActive(false);
            optionalButtonImage.rectTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            optionalPanel.SetActive(true);
            optionalButtonImage.rectTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    private IEnumerator startArrowPathfinding(Vector3 target)
    {
        // set arrow active and put on player location
        arrow.transform.position = playerPosition;
        // set arrow rotation
        Vector3 direction = target - arrow.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);


        arrow.SetActive(true);
        isPathFinding = true;
        // Move arrow to desination
        float duration = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            arrow.transform.position = Vector3.MoveTowards(arrow.transform.position, target, 5f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isPathFinding = false;
        arrow.SetActive(false);
    }


}
