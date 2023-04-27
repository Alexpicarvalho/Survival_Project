using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.OnFootActions _onFoot;
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _onFoot.Jump.performed += ctx => _playerMovement.Jump();
    }

    private void FixedUpdate()
    {
        _playerMovement.ProcessMovement(_onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _playerLook.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
