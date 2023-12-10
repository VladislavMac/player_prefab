using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyRotation : BasePlayerCameraRotation
{
    private void FixedUpdate()
    {
        CharacterRotate();
    }
}
