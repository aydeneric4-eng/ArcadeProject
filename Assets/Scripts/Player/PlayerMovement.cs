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
    private float AbsMin(float a, float b)
    {
        if (Mathf.Abs(a) == Mathf.Min(Mathf.Abs(a), Mathf.Abs(b)))
            return a;
        else
            return b;
    }
    private float AbsMax(float a, float b)
    {
        if (Mathf.Abs(a) == Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)))
            return a;
        else
            return b;
    }
    private float GetNewVelocityValue(float current,float input) // UGLY!!!!
    {
        float inputSign = Mathf.Sign(input);
        float currentSign = Mathf.Sign(current);
        if (input != 0) // There is an input
        {
            if (inputSign == currentSign || current == 0) // We are going in the direction of movement
            {
                if (Mathf.Abs(current) <= maxMoveSpeed)
                {
                    Debug.Log("There is input, moving in direction of vel, underspeed");
                    return 0f; // Under speed limit
                } else
                {
                    Debug.Log("There is input, moving in direction of vel, overspeed");
                    return 0f; // Over speed limit
                }
            } else
            {
                Debug.Log("There is input, moving away from direction of vel");
                return 0f; // Going against speed
            }
        }

        if (current == 0) // No input + already stropped
            return 0f;

        if (Mathf.Abs(current) <= maxMoveSpeed) // No Input
        {
            Debug.Log("There is no input, underspeed");
            return 0f; // Not Overspeed
        }
        else
        {
            Debug.Log("There is no input, overspeed");
            return 0f; // Overspeed
        }
    }

    private void FixedUpdate()
    {
        velocity = new Vector3(GetNewVelocityValue(velocity.x, inputMovementVector.x), 0,0);//new Vector3(GetNewVelocityValue(velocity.x,inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);

        selfRigidBody.linearVelocity = velocity;
        //Debug.Log(newVelocity);
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
