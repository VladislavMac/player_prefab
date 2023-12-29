using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    [SerializeField] private float _walkingSpeed = 4.5f;
    [SerializeField] private float _runningSpeed = 6.5f;
    [SerializeField] private float _jumpSpeed = 8.0f;
    [SerializeField] private float _gravity = 20.0f;

    [SerializeField] private Camera _playerCamera;

    [SerializeField] private float _lookSpeed = 2.0f;
    [SerializeField] private float _lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    private float _rotationX = 0;
    private float _movementDirectionY;

    private bool  _canMove = true;
    private bool  _isRunning;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right   = transform.TransformDirection(Vector3.right);

        _isRunning = Input.GetKey(KeyCode.LeftShift);
        _movementDirectionY = moveDirection.y;

        PlayerWalking(forward, right);
        PlayerJump();
        PlayerGravity();
        PlayerCamera();
    }

    private void PlayerWalking(Vector3 forward, Vector3 right)
    {
        float curSpeedX = _canMove ? Input.GetAxisRaw("Vertical") : 0;
        float curSpeedY = _canMove ? Input.GetAxisRaw("Horizontal") : 0;

        moveDirection = ((forward * curSpeedX) + (right * curSpeedY)).normalized;

        characterController.Move(moveDirection * (_isRunning ? _runningSpeed : _walkingSpeed) * Time.deltaTime);
    }

    private void PlayerCamera()
    {
        if (_canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);

            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
        }
    }

    private void PlayerJump()
    {
        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            moveDirection.y = _jumpSpeed;
        }


        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void PlayerGravity()
    {
        // Примените силу тяжести. Сила тяжести умножается на deltaTime дважды (один раз здесь и один раз ниже
        // когда направление перемещения умножается на deltaTime). Это потому, что должна быть применена сила тяжести
        // как ускорение (мс^-2)

        if (!characterController.isGrounded)
        {
            moveDirection.y -= _gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}