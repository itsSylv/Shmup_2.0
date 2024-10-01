using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Main;
    private UserInput _userInput;

    private Vector3 _moveDirection;
    public Vector3 MoveDirection { get { return _moveDirection; } }

    private void Awake()
    {
        if (Main != null)
        {
            Destroy(gameObject);
        }

        Main = this;
        
        _userInput = new UserInput();
    }

    private void OnEnable()
    {
        _userInput.Enable();
        _userInput.Game.Movement.performed += OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _moveDirection = value.ReadValue<Vector3>();
    }
}
