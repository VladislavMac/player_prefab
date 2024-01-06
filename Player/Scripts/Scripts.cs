using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [SerializeField] private float _playerSpeedWalk = 3.5f;
    [SerializeField] private float _playerSpeedRun = 6.0f;
    [SerializeField] private float _playerSpeedCrouching = 2f;

    [SerializeField] private float _playerJumpHeight = 8.0f;
    [SerializeField] private float _gravity = 25.0f;

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private float _lookSpeed = 2.0f;

    private CharacterController _characterController;
    Vector3 moveDirection     = Vector3.zero;
    Vector3 velocityDirection = Vector3.zero;

    private float _movementDirectionY;

    private float _playerCrouchHeight = 1f;
    private float _playerHeight = 2.0f;
    private float _playerSpeed;

    private float _velocity;

    private float _rotationX = 0;
    private float _lookXLimit = 90.0f;

    private bool _isPlayerRunning;
    private bool _isPlayerJumping;
    private bool _isPlayerGrounded;
    private bool _isPlayerCrouching;
    private bool _isPlayerCanMove = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
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

        _isPlayerRunning   = Input.GetKey(KeyCode.LeftShift);
        _isPlayerCrouching = Input.GetKey(KeyCode.LeftControl);
        _isPlayerJumping   = Input.GetKeyDown(KeyCode.Space);

        ControllPlayerGrounded();
        ControllPlayerSpeed();

        if ( _isPlayerCanMove )
        {
            PlayerStatesControl(forward, right);
        }
    }

    private void PlayerStatesControl(Vector3 forward, Vector3 right)
    {
        PlayerMove(forward, right);

        PlayerCrouch(_playerCamera);
        PlayerGravity();
        PlayerJump();

        if( _gravity != 0)
        {
            DoGravity();
        }

        PlayerCamera();
    }
    private void PlayerMove(Vector3 forward, Vector3 right)
    {
        float curSpeedX = Input.GetAxisRaw("Vertical");
        float curSpeedY = Input.GetAxisRaw("Horizontal");

        moveDirection = ((forward * curSpeedX) + (right * curSpeedY)).normalized * _playerSpeed;

        _characterController.Move(moveDirection * Time.deltaTime);
    }

    private void PlayerJump()
    {
        if (_isPlayerJumping && !_isPlayerCrouching && _isPlayerGrounded)
        {
            _velocity += _playerJumpHeight;
        }
    }
    private void PlayerCrouch(Camera playerCamera)
    {

        if (_isPlayerCrouching && _isPlayerGrounded)
        {
            _characterController.height = _playerCrouchHeight;
            _characterController.center = new Vector3(0, (_playerCrouchHeight / 2), 0);

        }
        else 
        {
            _characterController.height = _playerHeight;
            _characterController.center = new Vector3(0, 1, 0);
        }
    }

    private void PlayerGravity()
    {
        if (!_isPlayerGrounded)
        {
            _velocity += -_gravity * Time.deltaTime;
        }
        else if(_isPlayerGrounded)
        {
            _velocity = -1.0f;
        }
    }
    private void DoGravity()
    {
        velocityDirection.y = _velocity;
        _characterController.Move(velocityDirection * Time.deltaTime);
    }
    
    private void ControllPlayerGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f * 0.1f) || _characterController.isGrounded)
        {
            _isPlayerGrounded = true;
        }
        else
        {
            _isPlayerGrounded = false;
        }
    }

    private void ControllPlayerSpeed()
    {
        if(!_isPlayerCrouching)
        {
            _playerSpeed = _isPlayerRunning ? _playerSpeedRun : _playerSpeedWalk;
        }
        else if(_isPlayerCrouching)
        {
            _playerSpeed = _playerSpeedCrouching;
        }
    }

    private void PlayerCamera()
    {
        _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);

        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
    }
}