using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TimeGatingScript : MonoBehaviour, IPointerClickHandler
{
    // NOTE: when using this script, make sure the object name contains either "Statue" or "Book"
    // Following that, there also must be a number following after it
    // ex. "Statue1", "Statue2", "Book4"

    [SerializeField] private int year;
    [SerializeField] private int month;
    [SerializeField] private int day;
    [SerializeField] private int hour;
    [SerializeField] private int minute;
    [SerializeField] private int second;

    [SerializeField] private LoadingScreen asyncLoader;
    [SerializeField] private GameObject dateMessagePanel;

    DateTime timeGate;


    // Start is called before the first frame update
    void Start()
    {
        timeGate = new DateTime(year, month, day, hour, minute, second);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerCanAccess();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();
        if (now < timeGate)
        {
            dateMessagePanel.GetComponent<DateMessagePanel>().setDateText(timeGate.ToString());
            dateMessagePanel.SetActive(true);
        }
    }

    void CheckIfPlayerCanAccess()
    {

        DateTime now = DateTime.UtcNow.ToLocalTime();
        print(now);
        print(timeGate);

        // not allowed
        if (now < timeGate)
        {
            if (this.gameObject.GetComponent<StatueStages>() != null)
            {
                Destroy(this.gameObject.GetComponent<StatueStages>());
            }
            else if (this.gameObject.GetComponent<BookStages>() != null)
            {
                Destroy(this.gameObject.GetComponent<BookStages>());
            }

            if (this.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 100);
            }

        }
        else
        {
            if (this.gameObject.GetComponent<StatueStages>() == null && this.gameObject.name.Contains("Statue"))
            {
                StatueStages statueStages = this.gameObject.AddComponent<StatueStages>();
                statueStages.SetLoadingScreen(asyncLoader);
                statueStages.SetCurrentStage(Int32.Parse(this.gameObject.name.Substring(6)));

            }
            else if (this.gameObject.GetComponent<BookStages>() == null && this.gameObject.name.Contains("Book"))
            {
                BookStages bookStages = this.gameObject.AddComponent<BookStages>();
                bookStages.setLoadingScreen(asyncLoader);
                bookStages.SetCurrentStage(Int32.Parse(this.gameObject.name.Substring(4)));
            }

            if (this.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);
            }
        }

    }

}
