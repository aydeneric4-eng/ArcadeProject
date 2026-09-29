using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Transform selfTransform;
    private Rigidbody2D selfRigidBody;

    private Vector3 inputMovementVector = Vector3.zero;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
        selfRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        selfRigidBody.linearVelocity = inputMovementVector * moveSpeed;
        //selfTransform.position += inputMovementVector * moveSpeed * Time.deltaTime;
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        inputMovementVector = (Vector3)ctx.ReadValue<Vector2>();
    }
}
