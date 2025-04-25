using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFloat : MonoBehaviour
{
    [SerializeField] private bool isAltBob = true;

    [SerializeField] private float bobbingSpeed = 1.0f;
    [SerializeField] private float bobbingHeight = 0.5f;
    private Vector3 localStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        localStartPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newX;
        if (isAltBob)
        {
            newX = localStartPosition.x - Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        }
        else
        {
            newX = localStartPosition.x + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        }

        transform.localPosition = new Vector3(newX, localStartPosition.y, localStartPosition.z);
    }
}
