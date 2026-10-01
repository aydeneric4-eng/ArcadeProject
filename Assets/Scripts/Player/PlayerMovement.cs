using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxMoveSpeed = 5f;
    [SerializeField] float acceleration = 2.5f;
    [SerializeField] float deacceleration = 3f;
    [SerializeField] float overspeedDeacceleration = 5f;

    private Transform selfTransform;
    private Rigidbody2D selfRigidBody;

    private Vector3 inputMovementVector = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
        selfRigidBody = GetComponent<Rigidbody2D>();
    }

    private float GetAccType()
    {
        if (velocity.magnitude > maxMoveSpeed)
            return deacceleration;
        else
            return acceleration;
    }

    private float GetNewVelocityValue(float current,float input)
    {
        if (input != 0)
        {
            if (true) { }
        }

        return 0f;
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = new Vector3(velocity.x, velocity.y, 0);

        selfRigidBody.AddForce(inputMovementVector * maxMoveSpeed);
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        inputMovementVector = CustomUtilities.Vec2ToVec3(ctx.ReadValue<Vector2>());
    }
}
