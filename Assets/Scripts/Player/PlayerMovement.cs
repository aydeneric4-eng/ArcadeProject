using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxMoveSpeed = 5f;
    [SerializeField] float acceleration = 2.5f;
    [SerializeField] float deacceleration = 1f;
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

    private float PreferClosestToZero(float a, float b)
    {
        if (Mathf.Abs(a) < Mathf.Abs(b))
            return a;
        else 
            return b;
    }
    private float IsDesiredSign(float value, float desiredSign, float fallBack = 0)
    {
        if (Mathf.Sign(value) != desiredSign)
            return fallBack;
        else
            return value;
    }
    private float GetNewVelocityValue(float current,float input) // UGLY!!!!
    {
        if (input != 0) // There is an input
        {
            if (Mathf.Sign(input) == Mathf.Sign(current) || current == 0) // We are going in the direction of movement
            {
                if (current < maxMoveSpeed)
                {
                    return Mathf.Min(current + acceleration * input, maxMoveSpeed); // Under speed limit
                } else
                {
                    return Mathf.Max(IsDesiredSign(current - overspeedDeacceleration * Mathf.Sign(current), Mathf.Sign(overspeedDeacceleration * Mathf.Sign(current))), maxMoveSpeed); // Over speed limit
                }
            } else
            {
                return Mathf.Max(IsDesiredSign(current - Mathf.Max(acceleration,deacceleration) * Mathf.Sign(current), Mathf.Sign(Mathf.Max(acceleration, deacceleration) * Mathf.Sign(current))) * input, maxMoveSpeed); // Going against speed
            }
        }

        if (current < maxMoveSpeed) // No Input
        {
            return IsDesiredSign(current - deacceleration * Mathf.Sign(current), Mathf.Sign(deacceleration * Mathf.Sign(current))); // Not Overspeed
        }
        else
        {
            return IsDesiredSign(current - overspeedDeacceleration * Mathf.Sign(current), Mathf.Sign(overspeedDeacceleration * Mathf.Sign(current))); // Overspeed
        }
    }

    private void FixedUpdate()
    {
        Vector3 newVelocity = new Vector3(GetNewVelocityValue(velocity.x,inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);

        selfRigidBody.linearVelocity = newVelocity;
        Debug.Log(newVelocity);
    }


    public void Move(InputAction.CallbackContext ctx)
    {
        inputMovementVector = CustomUtilities.Vec2ToVec3(ctx.ReadValue<Vector2>());
        inputMovementVector = new Vector2(CustomUtilities.Sign(inputMovementVector.x), CustomUtilities.Sign(inputMovementVector.y)); // UNnormalizes
        /*
        Debug.Log("##### Movement #####");
        Debug.Log("XVOMP");
        Debug.Log(Mathf.Sign(inputMovementVector.x));
        Debug.Log("YCOMP");
        Debug.Log(Mathf.Sign(inputMovementVector.y));
        Debug.Log("##### END #####");
        */
    }
}
