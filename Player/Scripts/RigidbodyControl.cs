using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyControl : BasePlayerMove
{
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + playerVector * playerSpeed * Time.fixedDeltaTime);
    }
}
