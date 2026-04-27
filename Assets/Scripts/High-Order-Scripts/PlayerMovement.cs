using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private UIManagerTemplate UIManager;

    [SerializeField]
    Animator animator;
    [SerializeField]
    BoxCollider2D interactRange;
    public Finger MovementFinger;
    public Vector2 MovementAmount;
    private bool isUsingKeyboard = false;
    private float speed = 5.0f;
    [SerializeField]
    // This is for UI switching purposes
    private GameObject isJoystickPanelActive;

    private bool onPause = false;

    public void ResetMovement()
    {
        MovementAmount = Vector2.zero;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            onPause = true;
            return;
        }
        else if (onPause && Time.timeScale == 1)
        {
            onPause = false;
            ResetMovement();
        }

        if (!onPause)
        {
            HandleKeyboardInput();
            this.transform.Translate(speed * new Vector2(MovementAmount.x, 0) * Time.deltaTime);
            UpdateAnimator();
        }

    }

    private void HandleKeyboardInput()
    {
        if (!isJoystickPanelActive.activeSelf) { return; }
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            input.x += 1;
        }

        if (input != Vector2.zero)
        {
            isUsingKeyboard = true;
            input = input.normalized;
            MovementAmount = input;
        }
        else if (isUsingKeyboard)
        {
            MovementAmount = Vector2.zero;
        }
    }

    public void ClickInteractButton()
    {
        Debug.Log("Interact");
        // if interactRange is trigger touches Interactable tagged object do somethign
        if (interactRange.IsTouchingLayers(LayerMask.GetMask("Interactable")))
        {
            Debug.Log("Interacting with object");
        }
    }
    public void UpdateAnimator()
    {

        // x movement
        animator.SetFloat("Speed", MovementAmount.magnitude);
        //Add condition if player's lower hitbox is touching layermask ground

        if (MovementAmount.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (MovementAmount.x > 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public bool isAnyUICanvasOpen()
    {
        if (UIManager != null)
            return UIManager.isAnyUICanvasOpen();
        else return false;
    }


}