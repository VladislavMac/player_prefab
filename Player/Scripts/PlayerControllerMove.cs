using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerMove : BasePlayerMove
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    private CharacterController _playerController;
    private float _playerGravity = -9.81f;
    private Vector3 _velocity;

    bool isGrounded;
    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    private new void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (isGrounded && _velocity.y < 0) _velocity.y = -2f;

        base.Update();
        _playerController.Move(playerVector * playerSpeed * Time.deltaTime);

        _velocity.y += _playerGravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);
    }
}
