using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimeStamping
{
    public float start;
    public float end;
}
public class VoiceManager : MonoBehaviour
{
    public TimeStamping[] timeStamp;

    // expects line number without caring for indexing
    public TimeStamping GetTimeStamping(int lineNumber)
    {
        return timeStamp[lineNumber - 1];
    }

}


