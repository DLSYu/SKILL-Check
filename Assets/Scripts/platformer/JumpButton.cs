using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    private float jump = 335.0f;

    [SerializeField] Vector2 boxSize;
    [SerializeField] float castDistance;
    [SerializeField] LayerMask groundLayer;

    private bool wasInAir = false;
    
    private void Update()
    {
        // change condition to if player velocity is going upwards
        if(IsGrounded()){  
            animator.SetBool("InAir", false);

            if (wasInAir){
                animator.SetTrigger("landedTrigger");
                wasInAir = false;
            }
        }
        else{
            animator.ResetTrigger("landedTrigger");
            animator.ResetTrigger("jumpTrigger");
            animator.SetBool("InAir", true);
            wasInAir = true;
        }

        float verticalVelocity = player.GetComponent<Rigidbody2D>().velocity.y;
        animator.SetFloat("yVelocity", verticalVelocity);

    }
    public void OnPointerDown(PointerEventData eventData){
        
        if (IsGrounded()){
            animator.SetTrigger("jumpTrigger");
            Debug.Log("Jump");
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jump));
        }   
    }


    private bool IsGrounded()
    {
        //  RaycastHit2D hit = Physics2D.Raycast(player.GetComponent<BoxCollider2D>().bounds.center - (new Vector3(0f, player.GetComponent<BoxCollider2D>().size.y + 0.1f, 0f)/2), Vector2.down, 0.01f);
        //  return hit.collider != null;

        if(Physics2D.BoxCast(player.transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer)){
            return true;
        }
        else{
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(player.transform.position - transform.up * castDistance, boxSize);
    }

}

