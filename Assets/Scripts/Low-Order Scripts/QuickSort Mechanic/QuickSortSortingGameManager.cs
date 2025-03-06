using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSortSortingGameManager : MonoBehaviour
{
    //for Singleton
    public static QuickSortSortingGameManager instance { get; private set; }

    //for managing the entire game
    public static GameObject inventoryPanel { get; private set; }
    public static GameObject relicCheckedSlotsPanel { get; private set; }
    public static List<GameObject> relicParts { get; set; }

    //for managing the quick-sort mechanic
    public static GameObject pivot { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;

            DontDestroyOnLoad(this);
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

    //public static void SetupInitScene(GameObject initialRelicPartsContainer)
    //{
    //    List<GameObject> initRelicParts = new List<GameObject>();

    //    for (int i = 0; i < initialRelicPartsContainer.transform.childCount; i++)
    //    {
    //        initRelicParts.Add(initialRelicPartsContainer.transform.GetChild(i).gameObject);
    //    }
    //    Shuffle<GameObject>(ref initRelicParts);
    //    relicParts = initRelicParts;
    //}

    public static void Shuffle<T>(ref List<T> list)
    {
        int count = list.Count;
        int lastIndex = count - 1;
        int randIndex;
        for (int i = count - 1; i >= 0; i--)
        {
            randIndex = Random.Range(0, lastIndex);
            var temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
            lastIndex--;
        }
    }
}
