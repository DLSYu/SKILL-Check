using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSetUp : MonoBehaviour
{
    //for initial set-up
    [SerializeField] private GameObject initialRelicPartsContainer;
    [SerializeField] private GameObject initialRelicPanel;
    [SerializeField] private GameObject QuickSortSortingGameManagerObj;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> initRelicParts = new List<GameObject>();

        for (int i = 0; i < initialRelicPartsContainer.transform.childCount; i++)
        {
            GameObject relicPart = initialRelicPartsContainer.transform.GetChild(i).gameObject;
            relicPart.GetComponent<StorySegment>().order = i;
            initRelicParts.Add(relicPart);

            Debug.Log($"order = {i} ; {relicPart.name}");
        }

        QuickSortSortingGameManager.Instance.SetRelicPartsList(initRelicParts);
        initRelicParts = QuickSortSortingGameManager.Instance.shuffledRelicParts;

        for (int i = 0; i < initialRelicPanel.transform.childCount; i++)
        {
            initRelicParts[i].GetComponent<RelicMovement>().isMovable = false;

            QuickSortSortingGameManager.Instance.PutRelicPart(initialRelicPanel.transform.GetChild(i).gameObject, initRelicParts[i]);

            //RelicSlot relicSlot = initialRelicPanel.transform.GetChild(i).gameObject.GetComponent<RelicSlot>();
            //relicSlot.RemoveRelic();
            //GameObject toPlace = Instantiate(initRelicParts[i], initialRelicPanel.transform.GetChild(i));
            //relicSlot.PlaceRelic(toPlace);
            //toPlace.transform.localPosition = Vector3.zero;
        }

        StartCoroutine(tempNextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator tempNextScene()
    {
        yield return new WaitForSeconds(5f);

        //for (int i = 0; i < initialRelicPartsContainer.transform.GetChild(0).childCount; i++)
        //{
        //    DontDestroyOnLoad(initialRelicPartsContainer.transform.GetChild(i).gameObject);
        //}
        initialRelicPartsContainer.transform.SetParent(QuickSortSortingGameManager.Instance.transform);
        //initialRelicPanel.transform.SetParent(QuickSortSortingGameManagerObj.transform);

        SceneManager.LoadScene("QuickSort_PivotScene");
    }
}
