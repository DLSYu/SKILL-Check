using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSortSortingGameManager : MonoBehaviour
{
    //for Singleton
    public static QuickSortSortingGameManager Instance { get; private set; }

    //for managing the entire game
    public List<GameObject> relicParts { get; private set; }
    public List<GameObject> shuffledRelicParts { get; private set; }

    //for managing the quick-sort mechanic
    public int pivotRandIndex { get; private set; } = -1;

    private void Awake()
    {
        Screen.SetResolution(2000, 1200, true);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void Initialize()
    //{
    //    for (int i = 0; i < transform.GetChild(0).childCount; i++)
    //    {
    //        relicParts.Add(transform.GetChild(0).GetChild(i).gameObject);
    //    }

    //    shuffledRelicParts = relicParts;
    //    shuffledRelicParts = Shuffle<GameObject>(shuffledRelicParts);

    //    if (pivotRandIndex.IsUnityNull())
    //    {
    //        pivotRandIndex = Random.Range(2, 4);
    //    }
    //}

    public GameObject GetPivot()
    {
        if(pivotRandIndex == -1) ChoosePivot();
        Debug.Log($"relicParts Count: {relicParts.Count}");
        return relicParts[pivotRandIndex];
    }

    public List<T> Shuffle<T>( List<T> list)
    {
        List<T> listToShuffle = list;
        int count = listToShuffle.Count;
        int lastIndex = count - 1;
        int randIndex;
        for (int i = count - 1; i >= 0; i--)
        {
            randIndex = Random.Range(0, lastIndex);
            var temp = listToShuffle[i];
            listToShuffle[i] = listToShuffle[randIndex];
            listToShuffle[randIndex] = temp;
            lastIndex--;
        }
        return listToShuffle;
    }

    public GameObject PutRelicPart (GameObject relicSlot, GameObject relicPart)
    {
        RelicSlot relicSlotComp = relicSlot.GetComponent<RelicSlot>();
        relicSlotComp.RemoveRelic();
        for (int i = 0; i < relicSlot.transform.childCount; i++)
        {
            Destroy(relicSlot.transform.GetChild(i).gameObject);
        }

        GameObject relicPartInst = Instantiate(relicPart, relicSlot.transform);
        relicSlotComp.placedRelic = relicPartInst;
        relicPartInst.transform.localPosition = Vector3.zero;

        return relicPartInst;
        
    }

    public bool IsQuickSortCorrect (GameObject relicPart, bool checkForAfter)
    {
        int relicPartOrder = relicPart.GetComponent<StorySegment>().order;

        Debug.Log($"check {relicPartOrder} vs {GetPivot().gameObject.GetComponent<StorySegment>().order}");

        if (checkForAfter)
        {
            if (relicPartOrder > GetPivot().gameObject.GetComponent<StorySegment>().order) return true;

            return false;
        }
        else
        {
            if (relicPartOrder < GetPivot().gameObject.GetComponent<StorySegment>().order) return true;

            return false;
        }
    }

    public void SetRelicPartsList(List<GameObject> relicPartsToSet)
    {
        relicParts = new List<GameObject>();
        shuffledRelicParts = new List<GameObject>();

        relicParts.AddRange(relicPartsToSet);
        shuffledRelicParts.AddRange(Shuffle<GameObject>(relicPartsToSet));
    }

    private void ChoosePivot()
    {
        pivotRandIndex = Random.Range(2, 5);

        Debug.Log($"{pivotRandIndex}");
    }
}
