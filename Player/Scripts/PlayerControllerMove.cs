using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerControllerMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private CharacterController _playerController;
    [SerializeField] private float _playerSpeed = 6;

    [SerializeField] private float _gravity = 2;

    private Vector3 _playerVector;
    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _playerVector = (transform.right * Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical") * transform.forward).normalized;
        _playerVector -= (transform.up * _gravity * Time.deltaTime).normalized;
        _playerController.Move(_playerVector * _playerSpeed * Time.deltaTime);
    }
}
