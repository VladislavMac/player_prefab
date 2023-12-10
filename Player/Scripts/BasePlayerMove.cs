using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerMove : MonoBehaviour
{
    [SerializeField]

    protected float playerSpeed = 6;
    protected Vector3 playerVector;
    protected void Update() 
    { 
        playerVector = (transform.right * Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") * transform.forward).normalized; 
    }
}
