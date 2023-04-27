using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float _startMovementSpeed;
    [SerializeField] private float _maxMovementSpeed;
    public float _currentMovementSpeed;
    [SerializeField] private float _accelarationPerSecond;

    [Header("Jump Properties")]
    [SerializeField] float _jumpHeight;
    [SerializeField] float _groundCheckRaycastDistance;
    [SerializeField] LayerMask _whatIsGround = ~0;
    [SerializeField] float _airControlPercentage = 1;

    [Header("General Properties")]
    [SerializeField] float _gravity;

    [Header("References / Necessities")]
    private CharacterController _playerController;

    [Header("Runtime Properties")]
    [SerializeField] bool _isGrounded;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGrounded();
    }

    public void ProcessMovement(Vector2 input)
    {
        _movementDirection.x = input.x;
        _movementDirection.z = input.y;
        _movementDirection.y += _gravity * Time.deltaTime;

        if (!_isGrounded)
        {
            _movementDirection.x *= _airControlPercentage;
            _movementDirection.z *= _airControlPercentage;
        }
        if(_isGrounded && _movementDirection.y < 0) _movementDirection.y = -10f;

        _playerController.Move(_currentMovementSpeed * Time.deltaTime * transform.TransformDirection(_movementDirection));
    }

    public void Jump()
    {
        if(_isGrounded) _movementDirection.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, _groundCheckRaycastDistance, _whatIsGround))
        {
            _isGrounded = true;
            Debug.Log("Hitting : " + hit.transform.name);
        } 
        else _isGrounded = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * _groundCheckRaycastDistance);
    }
}