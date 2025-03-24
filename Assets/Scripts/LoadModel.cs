using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadModel : MonoBehaviour
{
    private AndroidJavaClass bertScoreEval;

    void Awake()
    {
        bertScoreEval = new AndroidJavaClass("com.skillcheck.bertscore_aar.BertScoreEval");
        bertScoreEval.CallStatic("loadModel");
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
