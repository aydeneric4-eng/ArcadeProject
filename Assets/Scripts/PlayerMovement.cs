using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Transform selfTransform;
    private Vector3 inputMovementVector = Vector3.zero;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        selfTransform.position += inputMovementVector * moveSpeed * Time.deltaTime;
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        inputMovementVector = (Vector3)ctx.ReadValue<Vector2>();
    }
}
