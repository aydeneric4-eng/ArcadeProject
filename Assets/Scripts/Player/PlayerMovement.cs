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

    private float PreferClosestToZero(float a, float b)
    {
        if (Mathf.Abs(a) < Mathf.Abs(b))
            return a;
        else 
            return b;
    }
    
    private float GetNewVelocityValue(float current,float input) // UGLY!!!!
    {
        Debug.Log(input);
        float inputSign = Mathf.Sign(input);
        float currentSign = Mathf.Sign(current);

        float absInput = Mathf.Abs(input);
        float absCurrent = Mathf.Abs(current);

        float modifyingValue = 0f;
        float modifySign = 1f;

        float finalValue = 0f;
        float finalSign = 1f;

        if (current == 0 && input == 0)
            return 0f;

        if (absCurrent > maxMoveSpeed)
            { modifyingValue = overspeedDeacceleration; modifySign = -1f; }
        else if (inputSign == currentSign && input != 0)
            { modifyingValue = acceleration; modifySign = 1f; }
        else
            { modifyingValue = deacceleration; modifySign = -1f; }

        if (input != 0 && inputSign != currentSign && absCurrent < modifyingValue) // No input + moving opp of current + change greater than current
            finalSign = -currentSign;
        else
            finalSign = currentSign;

        finalValue = Mathf.Max(Mathf.Min(absCurrent + modifyingValue * modifySign, maxMoveSpeed),0) * finalSign;


        Debug.Log("signs:");
        Debug.Log(input);
        Debug.Log(inputSign);
        Debug.Log(currentSign);
        Debug.Log("mod:");
        Debug.Log(modifyingValue);
        Debug.Log(modifySign);
        Debug.Log("finalValue");
        Debug.Log(finalValue);
        Debug.Log(finalSign);
        return finalValue;

        if (input != 0) // There is an input
        {
            if (inputSign == currentSign || current == 0) // We are going in the direction of movement
            {
                if (Mathf.Abs(current) <= maxMoveSpeed)
                {
                    Debug.Log("There is input, moving in direction of vel, underspeed");
                    return CustomUtilities.AbsMin(current + acceleration * currentSign, maxMoveSpeed); // Under speed limit
                } else
                {
                    Debug.Log("There is input, moving in direction of vel, overspeed");
                    return CustomUtilities.AbsMax(current - overspeedDeacceleration * currentSign, maxMoveSpeed); // Over speed limit
                }
            } else
            {
                Debug.Log("There is input, moving away from direction of vel");
                return current - Mathf.Max(acceleration, deacceleration) * currentSign; // Going against speed
            }
        }

        if (current == 0) // No input + already stropped
            return 0f;

        if (Mathf.Abs(current) <= maxMoveSpeed) // No Input
        {
            Debug.Log("There is no input, underspeed");
            return CustomUtilities.IsDesiredSign(current - deacceleration * currentSign, currentSign); // Not Overspeed
        }
        else
        {
            Debug.Log("There is no input, overspeed");
            return CustomUtilities.IsDesiredSign(current - overspeedDeacceleration * currentSign, currentSign); // Overspeed
        }
    }

    private void FixedUpdate()
    {
        velocity = new Vector3(GetNewVelocityValue(velocity.x, inputMovementVector.x), 0,0);//new Vector3(GetNewVelocityValue(velocity.x,inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);

        selfRigidBody.linearVelocity = velocity;

        //Debug.Log("CustomUtilities.AbsMin(0, 0)");
        //Debug.Log(CustomUtilities.AbsMin(-10, -5));
        //Debug.Log(CustomUtilities.AbsMax(-10, -5));
        //Debug.Log("CustomUtilities.AbsMin(0, 0)");
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
