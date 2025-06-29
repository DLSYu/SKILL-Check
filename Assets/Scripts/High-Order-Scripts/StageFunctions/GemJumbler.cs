using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemJumbler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gemParents = new List<GameObject>();
    [SerializeField]
    private List<GameObject> gems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        List<GameObject> gemParentsCopy = new List<GameObject>(gemParents);
        for (int i = 0; i < gems.Count; i++)
        {
            int randomizedNum = Random.Range(0, gems.Count - i);
            Debug.Log(gems.Count - i);
            Debug.Log("i[" + i + "] " + randomizedNum);
            gems[i].transform.SetParent(gemParentsCopy[randomizedNum].transform, false);
            gemParentsCopy.RemoveAt(randomizedNum);
        }

    }

}
