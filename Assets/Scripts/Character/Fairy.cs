using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fairy : MonoBehaviour
{
    [SerializeField] private float bobbingSpeed = 1f;
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
        float newY = localStartPosition.y + Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;
        transform.localPosition = new Vector3(localStartPosition.x, newY, localStartPosition.z);
    }
}