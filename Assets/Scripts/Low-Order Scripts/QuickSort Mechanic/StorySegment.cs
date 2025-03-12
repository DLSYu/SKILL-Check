using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySegment : MonoBehaviour
{
    public int order;
    [SerializeField] private bool isRead = false;
    [TextArea] [SerializeField] private string storySegment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStorySegment()
    {
        isRead = true;
    }
}
