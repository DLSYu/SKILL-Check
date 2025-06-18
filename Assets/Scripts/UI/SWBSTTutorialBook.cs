using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SWBSTTutorialBook : MonoBehaviour, IPointerClickHandler, IDataPersistence
{
    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;
    private int pageIndex = 0;

    [SerializeField] private GameObject[] SWBSTPageObjects;
    [SerializeField] private GameObject SWBSTPageHolder;

    [SerializeField] private float timer = 0;
    [SerializeField] private List<float> allSWBSTtimer;

    void Update()
    {
        if (pageIndex == 0)
        {
            previousPageButton.SetActive(false);
            nextPageButton.SetActive(true);
        }
        else if (pageIndex == SWBSTPageObjects.Length - 1)
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(false);
        }
        else
        {
            previousPageButton.SetActive(true);
            nextPageButton.SetActive(true);

        }

        if (SWBSTPageHolder.activeInHierarchy)
        {
            timer += Time.deltaTime;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        SWBSTPageHolder.SetActive(true);
    }

    public void NextPage()
    {
        if (pageIndex + 1 < SWBSTPageObjects.Length)
        {
            pageIndex++;

        }
        SetPageActive();


    }

    public void PreviousPage()
    {
        if (pageIndex - 1 >= 0)
        {
            pageIndex--;

        }
        SetPageActive();
    }

    public void DisableSWBSTUI()
    {
        pageIndex = 0;
        SetPageActive();
        SWBSTPageHolder.SetActive(false);

        allSWBSTtimer.Add(timer);
        timer = 0;


    }

    void SetPageActive()
    {

        for (int i = 0; i < SWBSTPageObjects.Length; i++)
        {
            if (i == pageIndex)
                SWBSTPageObjects[i].SetActive(true);
            else
                SWBSTPageObjects[i].SetActive(false);

        }


    }

    public void LoadData(GameData data)
    {

    }

    public void SaveData(GameData data)
    {
        for (int i = 0; i < allSWBSTtimer.Count; i++)
            data.swbstInLobbyTime.Add(allSWBSTtimer[i]);
    }
}
