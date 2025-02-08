using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        this.cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        this.cam.transform.position = new Vector3(target.position.x, target.position.y + 3, -10);
    }
}
