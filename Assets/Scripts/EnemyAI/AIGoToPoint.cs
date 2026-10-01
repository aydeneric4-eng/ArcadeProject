using UnityEngine;

[RequireComponent(typeof(RBMovement))]
public class AIGoToPoint : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float maxMoveAngleDifference = 0.1f;
    [SerializeField] float variance = 0f;
    [SerializeField] float minDistanceToTarget = 0.5f;
    [SerializeField] bool active = false;

    private Transform selfTransform;
    private RBMovement selfRBM;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
        selfRBM = GetComponent<RBMovement>();
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
            Vector3 inputVect = (Vector3)CustomUtilities.GetVectorByAngleAndDistance(1, CustomUtilities.GetAngleOf2DVect((Vector2)selfTransform.position, (Vector2)targetTransform.position) + Random.Range(-variance,variance));
            Debug.Log(inputVect);
            selfRBM.inputMovementVector = inputVect;
        }
        
    }
}
