using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BagCloseOpen : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<RelicMovement>().IsUnityNull())
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0)
            {
                animator.Play("Opening", 0, 0);
            }
            animator.SetFloat("animSpeedMult", 1f);
            Debug.Log("anim timestamp: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.GetComponent<RelicMovement>().IsUnityNull())
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                animator.Play("Opening", 0, 1);
            }
            animator.SetFloat("animSpeedMult", -1f);
            Debug.Log("anim timestamp: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime.ToString());
        }
    }
}
