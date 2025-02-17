using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject player;
    private float jump = 300.0f;
    public void OnPointerDown(PointerEventData eventData){

        if (IsGrounded())
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jump));

    }

    private bool IsGrounded()
    {
         RaycastHit2D hit = Physics2D.Raycast(player.GetComponent<BoxCollider2D>().bounds.center - (new Vector3(0f, player.GetComponent<BoxCollider2D>().size.y + 0.1f, 0f)/2), Vector2.down, 0.1f);
        
         Debug.Log(hit.collider);
         return hit.collider != null;
    }
}
