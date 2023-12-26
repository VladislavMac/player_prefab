using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    protected float sensitivity = 1.5f;

    [SerializeField]
    protected float smooth = 10;

    [SerializeField]
    protected Transform character;

    protected float YRotation;
    protected float XRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        CharacterRotate();
    }

    private void Update()
    {
        YRotation += Input.GetAxis("Mouse X") * sensitivity;
        XRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        CharacterRotate();
    }

    private void CharacterRotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(XRotation, YRotation, 0), Time.deltaTime * smooth);
        character.rotation = Quaternion.Euler(0, YRotation, 0);
    }
}
