using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LateStage
{
    [SerializeField] public List<string> swbstOrFreeformList = new List<string>();
    [SerializeField] public List<HO_SubmitAttempt> attempts = new List<HO_SubmitAttempt>();
    [SerializeField] public List<float> scoresList = new List<float>();
}
