using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRotation : BasePlayerCameraRotation
{
    private new void Update()
    {
        base.Update();
        CharacterRotate();
    }
}
