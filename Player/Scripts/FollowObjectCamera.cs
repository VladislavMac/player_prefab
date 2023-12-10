using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectCamera : MonoBehaviour
{
    [SerializeField]
    private Transform fpsViewTransform;

    private void Update()
    {
        transform.position = fpsViewTransform.position;
    }
}
