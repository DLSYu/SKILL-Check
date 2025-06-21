using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class TimeCheckInstance : MonoBehaviour
{
    public List<DateTime[]> allowedTimes = new List<DateTime[]>();
    public static TimeCheckInstance instance { get; private set; }

    public string[] weekPasswords = new string[]
    {
        "test1",
        "test2",
        "test3",
        "test4",
        "test5"

    };

    DateTime[] week1 = new DateTime[]
    {
            new DateTime(2025, 2, 21, 11, 0, 0),
            new DateTime(2025, 2, 28, 11, 15, 0)
    };

    DateTime[] week2 = new DateTime[]
    {
            new DateTime(2025, 3, 3, 11, 0, 0),
            new DateTime(2025, 3, 20, 11, 15, 0)
    };
    DateTime[] week3 = new DateTime[]
    {
                new DateTime(2025, 4, 1, 11, 0, 0),
                new DateTime(2025, 4, 7, 11, 15, 0)
    };

    DateTime[] week4 = new DateTime[]
    {
                new DateTime(2025, 5, 1, 11, 0, 0),
                new DateTime(2025, 5, 8, 11, 15, 0)
    };

    DateTime[] week5 = new DateTime[]
    {
                new DateTime(2025, 6, 21, 16, 15, 0),
                new DateTime(2025, 6, 22, 11, 15, 0)
    };

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one TimeCheckInstance in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        allowedTimes.Add(week1);
        allowedTimes.Add(week2);
        allowedTimes.Add(week3);
        allowedTimes.Add(week4);
        allowedTimes.Add(week5);
        instance = this;

    }

    public string IdentifyCurrentWeekPassword()
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();
        string currentPassword = weekPasswords[0];
        for (int i = 0; i < allowedTimes.Count; i++)
        {
            if (now > allowedTimes[i][0])
                currentPassword = weekPasswords[i];
            else
            {
                break;
            }
        }
        // Debug.Log("password: " + currentPassword);
        return currentPassword;
    }

    public DateTime GetNextPlaySession()
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();
        DateTime temp = DateTime.MinValue;
        for (int i = 0; i < allowedTimes.Count; i++)
        {
            if (now < allowedTimes[i][0])
            {
                temp = allowedTimes[i][0];
                break;
            }
        }
        return temp;
    }

    public bool isWithinAllowedTimes()
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();
        bool foundAllowedTime = false;
        for (int i = 0; i < allowedTimes.Count; i++)
        {
            if (now > allowedTimes[i][0] && now <= allowedTimes[i][1])
            {
                foundAllowedTime = true;
                break;
            }


        }
        return foundAllowedTime;
    }




}