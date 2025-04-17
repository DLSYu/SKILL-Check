using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StorySegment : MonoBehaviour
{
    public int order;
    [SerializeField] private bool isRead = false;
    [TextArea] [SerializeField] private string storySegment;

    [SerializeField] private GameObject relicPopupPanel; // Reference to the pop-up panel
    [SerializeField] private TextMeshProUGUI relicText; // Reference to the text component

    // Start is called before the first frame update
    void Start()
    {

        if (relicPopupPanel.activeSelf)
        {
            relicPopupPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStorySegment()
    {
        Debug.Log("reading segment...");
        Debug.Log($"relicText == null: {relicText == null}");
        Debug.Log($"storySegment == null: {storySegment == null}");
        relicText.text = storySegment;
        relicPopupPanel.SetActive(true);

        isRead = true;
    }

    public void OnCloseButton()
    {
        relicPopupPanel.SetActive(false);
    }
}
