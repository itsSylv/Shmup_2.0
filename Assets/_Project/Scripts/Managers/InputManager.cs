using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Main;
    private UserInput _userInput;

    public Action OnFire;
    public Action OnSpecialFire;
    public Action OnPause;
    
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
        EnableUserInput();
    }

    private void EnableUserInput()
    {
        _userInput.Enable();
        
        _userInput.Game.Movement.performed += OnMovementPerformed;
        _userInput.Game.Shooting.performed += OnFirePerformed;
        _userInput.Game.SpecialFire.performed += OnSpecialFirePerformed;
        _userInput.Game.Pausing.performed += OnPausePerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _moveDirection = value.ReadValue<Vector3>();
    }

    private void OnFirePerformed(InputAction.CallbackContext value)
    {
        OnFire?.Invoke();
    }

    private void OnSpecialFirePerformed(InputAction.CallbackContext value)
    {
        OnSpecialFire?.Invoke();
    }

    private void OnPausePerformed(InputAction.CallbackContext value)
    {
        OnPause?.Invoke();
    }
}
