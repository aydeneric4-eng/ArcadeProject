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
        float inputSign = Mathf.Sign(input);
        float currentSign = Mathf.Sign(current);

        float absInput = Mathf.Abs(input);
        float absCurrent = Mathf.Abs(current);

        float clampMax = maxMoveSpeed;
        float clampMin = 0;

        float modifyingValue = 0f;
        float modifySign = 1f;

        float finalValue = 0f;
        float finalSign = 1f;

        if (current == 0 && input == 0)
            return 0f;

        if (absCurrent > maxMoveSpeed)
        {
            modifyingValue = overspeedDeacceleration;
            modifySign = -1f;
            clampMax = overspeedDeacceleration + maxMoveSpeed;
        }
        else if ((inputSign == currentSign || current == 0) && input != 0)
        {
            modifyingValue = acceleration;
            modifySign = 1f;
        }
        else if (inputSign != currentSign && current != 0 && acceleration > deacceleration && input != 0)
        {
            modifyingValue = acceleration;
            modifySign = -1f;
        }
        else if (input == 0 && current != 0)
        {
            modifyingValue = deacceleration;
            modifySign = -1f;
        }

        if (input != 0 && inputSign != currentSign && absCurrent < modifyingValue) // No input + moving opp of current + change greater than current
            finalSign = -currentSign;
        else
            finalSign = currentSign;

        finalValue = Mathf.Max(Mathf.Min(absCurrent + modifyingValue * modifySign, clampMax), clampMin) * finalSign;

        //Debug.Log("signs:");
        //Debug.Log(input);
        //Debug.Log(inputSign);
        //Debug.Log(currentSign);
        //Debug.Log("clamps:");
        //Debug.Log(clampMax);
        //Debug.Log(clampMin);
        //Debug.Log("mod:");
        //Debug.Log(modifyingValue);
        //Debug.Log(modifySign);
        //Debug.Log("finalValue");
        //Debug.Log(finalValue);
        //Debug.Log(finalSign);
        return finalValue;
    }

    private void FixedUpdate()
    {
        velocity = new Vector3(GetNewVelocityValue(velocity.x, inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);//new Vector3(GetNewVelocityValue(velocity.x,inputMovementVector.x), GetNewVelocityValue(velocity.y, inputMovementVector.y), 0);

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
