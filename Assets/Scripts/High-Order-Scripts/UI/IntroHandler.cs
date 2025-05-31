using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class IntroHandler : MonoBehaviour
{

    [Header("Disable Player Movement")]
    [SerializeField] private GameObject player;

    [Header("Editable Objectives")]
    [SerializeField] private string objective;
    [SerializeField] private int gemAmount;

    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI gemText;

    // Start is called before the first frame update
    void Start()
    {

        if (player != null)
            player.GetComponent<PlayerMovement>().enabled = false;

        if (objective == "")
        {
            objective = "Get through the door!";
        }
        Time.timeScale = 0;
        objectiveText.text = "Objective:\n" + objective;
        gemText.text = "Gems to collect: " + gemAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DismissUI()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);

        if (player != null)
            player.GetComponent<PlayerMovement>().enabled = true;
        Destroy(this.gameObject);
    }
}
