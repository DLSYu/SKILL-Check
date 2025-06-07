using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SWBSTTutorialBook : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;
    private int pageIndex = 0;

    [SerializeField] private GameObject[] SWBSTPageObjects;
    [SerializeField] private GameObject SWBSTPageHolder;

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
}
