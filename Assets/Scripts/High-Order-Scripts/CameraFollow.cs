using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        this.cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        this.cam.transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
        // this.cam.transform.position = new Vector3(target.position.x + 2f, target.position.y + 2f, -11f);
    }
}
