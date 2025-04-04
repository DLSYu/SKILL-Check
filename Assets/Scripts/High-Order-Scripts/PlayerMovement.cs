using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public Vector2 joystick_size = new Vector2(300, 300);
    [SerializeField]
    public FloatingJoystick Joystick;
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

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (!isJoystickPanelActive.activeSelf) { return; }

        if (MovedFinger == MovementFinger)
        {
            isUsingKeyboard = false;
            Vector2 knobPosition;
            float maxMovement = joystick_size.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            // Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (!isJoystickPanelActive.activeSelf) { return; }

        if (MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            isUsingKeyboard = false;
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            // Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = joystick_size;
            // Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < joystick_size.x / 2)
        {
            StartPosition.x = joystick_size.x / 2;
        }

        if (StartPosition.y < joystick_size.y / 2)
        {
            StartPosition.y = joystick_size.y / 2;
        }
        else if (StartPosition.y > Screen.height - joystick_size.y / 2)
        {
            StartPosition.y = Screen.height - joystick_size.y / 2;
        }

        return StartPosition;
    }

    private void Update()
    {
        HandleKeyboardInput();
        this.transform.Translate(speed * new Vector2(MovementAmount.x, 0) * Time.deltaTime);
        UpdateAnimator();

    }

    private void HandleKeyboardInput()
    {
        if (!isJoystickPanelActive.activeSelf) { return; }
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            input.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }

        if (input != Vector2.zero)
        {
            isUsingKeyboard = true;
            input = input.normalized;
            MovementAmount = input;
            Joystick.Knob.anchoredPosition = input * (joystick_size.x / 2f);
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


}