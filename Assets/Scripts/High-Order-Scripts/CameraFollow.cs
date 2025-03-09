using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        this.cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = playerPosition.position + offset;
        // this.cam.transform.position = new Vector3(playerPosition.position.x + offset.x, playerPosition.position.y + offset.y, playerPosition.position.z + offset.z);
        // this.cam.transform.position = new Vector3(target.position.x + 2f, target.position.y + 2f, -11f);
        float smoothX;
        if(playerMovement.MovementAmount.x >0){
            smoothX = Mathf.SmoothDamp(this.cam.transform.position.x, targetPosition.x + 1f, ref velocity.x, 0.1f);
        }
        else if(playerMovement.MovementAmount.x < 0){
            smoothX = Mathf.SmoothDamp(this.cam.transform.position.x, targetPosition.x - 1f, ref velocity.x, 0.1f);
        }
        else{
            smoothX = Mathf.SmoothDamp(this.cam.transform.position.x, targetPosition.x, ref velocity.x, 0.1f);
        }
        
        this.cam.transform.position = new Vector3(smoothX, targetPosition.y, targetPosition.z);

    }
}
