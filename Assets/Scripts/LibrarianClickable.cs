using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LibrarianClickable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject librarianDialogue;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!librarianDialogue.activeInHierarchy)
            librarianDialogue.SetActive(true);
    }
}

