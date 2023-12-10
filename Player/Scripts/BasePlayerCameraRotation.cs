using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasePlayerCameraRotation : MonoBehaviour
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

    protected void Update()
    {
        YRotation += Input.GetAxis("Mouse X") * sensitivity; 
        XRotation -= Input.GetAxis("Mouse Y") * sensitivity; 

        XRotation = Mathf.Clamp(XRotation, -90f, 90f);
    }
        
    protected void CharacterRotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(XRotation, YRotation, 0), Time.deltaTime * smooth);
        character.rotation = Quaternion.Lerp(character.rotation, Quaternion.Euler(0, YRotation, 0), Time.deltaTime * smooth );
    }
}
