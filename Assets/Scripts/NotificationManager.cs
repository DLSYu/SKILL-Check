/*
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;


public class NotificationManager : MonoBehaviour
{

    [SerializeField] PushNotificationManager pushNotificationManager;

    private void Start()
    {
        pushNotificationManager.RequestAuthorization();
        pushNotificationManager.RegisterNotificationChannel();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            DateTime now = DateTime.UtcNow.ToLocalTime();
            DateTime date = TimeCheckInstance.instance.GetNextPlaySession(now);

            if (date != DateTime.MinValue)
            {
                AndroidNotificationCenter.CancelAllNotifications();
                pushNotificationManager.SendNotification("Babaylan Tales", "Tumatawag uli ang librarian, humanda ka na sa loob ng 30 minuto!", date.AddMinutes(-30));
            }
        }
    }
}
*/
