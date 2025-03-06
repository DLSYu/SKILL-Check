using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSetUp : MonoBehaviour
{
    //for initial set-up
    [SerializeField] private GameObject initialRelicPartsContainer;
    [SerializeField] private GameObject initialRelicPanel;

    private void Awake()
    {
        List<GameObject> initRelicParts = new List<GameObject>();

        for (int i = 0; i < initialRelicPartsContainer.transform.childCount; i++)
        {
            initRelicParts.Add(initialRelicPartsContainer.transform.GetChild(i).gameObject);
        }
        QuickSortSortingGameManager.Shuffle<GameObject>(ref initRelicParts);
        QuickSortSortingGameManager.relicParts = initRelicParts;

        for (int i = 0; i < initialRelicPanel.transform.childCount; i++)
        {
            RelicSlot relicSlot = initialRelicPanel.transform.GetChild(i).gameObject.GetComponent<RelicSlot>();
            initRelicParts[i].transform.SetParent(initialRelicPanel.transform.GetChild(i));
            initRelicParts[i].transform.localPosition = Vector3.zero;
            relicSlot.PlaceRelic(initRelicParts[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
