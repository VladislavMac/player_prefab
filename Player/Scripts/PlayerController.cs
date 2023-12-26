using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 6;
    [SerializeField] private float _gravity = 10;

    private CharacterController _playerController;

    private Vector3 _playerVector;
    private Vector3 _velocity;

    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _playerVector = (transform.right * horizontal + vertical * transform.forward).normalized;

        Walk(_playerVector);
        SetGravity(_playerController.isGrounded);
    }

    private void FixedUpdate()
    {
        Walk(_playerVector);
        SetGravity(_playerController.isGrounded);
    }

    private void Walk(Vector3 derect)
    {
        _playerController.Move(derect * _playerSpeed * Time.fixedDeltaTime);
    }

    private void SetGravity(bool isGrounded)
    {
        if( isGrounded)
        {
            _velocity.y = -1f;
        }
        _velocity.y -= _gravity * Time.fixedDeltaTime;
        _playerController.Move(_velocity * Time.fixedDeltaTime);
    }
}
