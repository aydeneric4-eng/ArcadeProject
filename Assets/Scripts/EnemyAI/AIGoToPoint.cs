using UnityEngine;

[RequireComponent(typeof(RBMovement))]
public class AIGoToPoint : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float maxVectComponentDifference = 0.05f;
    [SerializeField] float minDistanceToTarget = 0.5f;
    [SerializeField] bool active = false;

    private Transform selfTransform;
    private RBMovement selfRBM;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
        selfRBM = GetComponent<RBMovement>();
    }

    private float IsValueOutOfRange(float value, float maxDeviance, float inRangeReturn = 0f)
    {
        if (Mathf.Abs(value) < maxDeviance)
            return inRangeReturn;
        else
            return value;
    }

    private void FixedUpdate()
    {
        if ((selfTransform.position - targetTransform.position).magnitude < minDistanceToTarget)
        {
            selfRBM.inputMovementVector = Vector3.zero;
            return;
        }
        if (active)
        {
            Vector3 inputVect = (Vector3)CustomUtilities.GetVectorByAngleAndDistance(1, CustomUtilities.GetAngleOf2DVect((Vector2)selfTransform.position, (Vector2)targetTransform.position));

            inputVect = new Vector3(IsValueOutOfRange(inputVect.x, maxVectComponentDifference), IsValueOutOfRange(inputVect.y, maxVectComponentDifference), 0);
            
            //Debug.Log(inputVect);
            selfRBM.inputMovementVector = inputVect;
        }
        
    }
}
