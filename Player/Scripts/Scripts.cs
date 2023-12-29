using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed = 3.5f;
    [SerializeField] private float _runningSpeed = 6.0f;
    [SerializeField] private float _jumpSpeed = 5.0f;
    [SerializeField] private float _gravity = 20.0f;

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private float _lookSpeed = 2.0f;
    [SerializeField] private float _lookXLimit = 90.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    private float _rotationX = 0;
    private float _movementDirectionY;
    
    private bool _isPlayerRunning;
    private bool _isPlayerJumping;
    private bool _isPlayerGrounded;
    private bool _isPlayerCanMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerStates();
    }

    private void PlayerStates()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        _movementDirectionY = moveDirection.y;

        _isPlayerRunning = Input.GetKey(KeyCode.LeftShift);
        _isPlayerJumping = Input.GetKeyDown(KeyCode.Space);

        if( _isPlayerCanMove )
        {
            PlayerStatesControl(forward, right);
        }

        PlayerStatesPhysical(forward, right);
    }

    private void PlayerStatesControl(Vector3 forward, Vector3 right)
    {
        PlayerWalking(forward, right, (_isPlayerRunning ? _runningSpeed : _walkingSpeed));
        PlayerJump();
        PlayerCamera();
    }

    private void PlayerStatesPhysical(Vector3 forward, Vector3 right)
    {
        PlayerGrounded();
        PlayerGravity();
    }

    private void PlayerWalking(Vector3 forward, Vector3 right, float playerSpeed)
    {
        float curSpeedX = Input.GetAxisRaw("Vertical");
        float curSpeedY = Input.GetAxisRaw("Horizontal");

        moveDirection = ((forward * curSpeedX) + (right * curSpeedY)).normalized;

        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
    }

    private void PlayerCamera()
    {
        _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);

        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
    }

    private void PlayerJump()
    {
        if (_isPlayerJumping && _isPlayerGrounded)
        {
            moveDirection.y = _jumpSpeed;
        }
        else
        {
            moveDirection.y = _movementDirectionY;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void PlayerGravity()
    {
        if (!_isPlayerGrounded)
        {
            moveDirection.y -= _gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void PlayerGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.4f))
        {
            _isPlayerGrounded = true;
        }
        else
        {
            _isPlayerGrounded = false;
        }
    }
}