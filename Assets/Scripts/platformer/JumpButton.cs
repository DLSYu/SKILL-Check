using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    private float jump = 335.0f;

    [SerializeField] Vector2 allowJumpBoxSize;
    [SerializeField] float allowJumpCastDistance;
    [SerializeField] Vector2 allowWalkBoxSize;
    [SerializeField] float allowWalkCastDistance;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioSource audioSource;

    private bool triggerLandSound;
    private void Update()
    {
        float verticalVelocity = player.GetComponent<Rigidbody2D>().velocity.y;
        animator.SetFloat("yVelocity", verticalVelocity);
        // change condition to if player velocity is going upwards
        if (allowJump()){  
            animator.SetBool("nearLand", true);

            if(allowWalk()){  
            animator.SetBool("onLand", true);
            }
            else{
                animator.SetBool("onLand", false);
            }
        }
        else{
            animator.SetBool("nearLand", false);
        }

        

        

    }
    public void OnPointerDown(PointerEventData eventData){
        
        if (allowJump()){
            animator.SetTrigger("jumpTrigger");
            Debug.Log("Jump");
            audioSource.PlayOneShot(jumpSound);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, jump));
            animator.ResetTrigger("jumpTrigger");
            triggerLandSound = true;
        }   
    }


    private bool allowJump()
    {
        //  RaycastHit2D hit = Physics2D.Raycast(player.GetComponent<BoxCollider2D>().bounds.center - (new Vector3(0f, player.GetComponent<BoxCollider2D>().size.y + 0.1f, 0f)/2), Vector2.down, 0.01f);
        //  return hit.collider != null;

        if(Physics2D.BoxCast(player.transform.position, allowJumpBoxSize, 0f, -transform.up, allowJumpCastDistance, groundLayer)){
            if(triggerLandSound){
                audioSource.PlayOneShot(landSound);
                triggerLandSound = false;
            }
            return true;
        }
        else{
            return false;
        }
    }

    private bool allowWalk(){
        if(Physics2D.BoxCast(player.transform.position, allowWalkBoxSize, 0f, -transform.up, allowWalkCastDistance, groundLayer)){
            return true;
        }
        else{
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(player.transform.position - transform.up * allowJumpCastDistance, allowJumpBoxSize);
        Gizmos.DrawWireCube(player.transform.position - transform.up * allowWalkCastDistance, allowWalkBoxSize);
    }

}

