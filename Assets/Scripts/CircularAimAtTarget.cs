using Unity.VisualScripting;
using UnityEngine;

public class CircularAimAtTarget : MonoBehaviour
{
    [SerializeField] Transform parentTransform;
    [SerializeField] bool isTargetMouse;
    [SerializeField] Transform targetTransform;
    [SerializeField] bool rotateTowardsTarget;
    [SerializeField] bool moveTowardsTarget;
    [SerializeField] float maxMoveDistance;
    [SerializeField] float minMoveDistance;

    private Transform selfTransform;
    private Vector2 targetPosition;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (isTargetMouse)
        {
            Debug.LogWarning("MOUSE NOT IMPLIMENTED");
        }
        else
        {
            targetPosition = (Vector2)targetTransform.position;
        }
        Vector2 selfPos = (Vector2)selfTransform.position;
        float angleToTarget = GetAngle2Vects(parentTransform.position, targetPosition);

        if (rotateTowardsTarget)
        {
            transform.eulerAngles = new Vector3(0,0,angleToTarget);
        }
        if (moveTowardsTarget)
        {
            //selfTransform.position = parentTransform.position - targetTransform.position;
            selfTransform.position = parentTransform.position + new Vector3(Mathf.Cos(angleToTarget * Mathf.Deg2Rad) * maxMoveDistance, Mathf.Sin(angleToTarget*Mathf.Deg2Rad)*maxMoveDistance, 0);
        }

    }

    private float GetAngle2Vects(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 directonVect = targetPosition - startPos;
        return Mathf.Atan2(directonVect.y, directonVect.x) * Mathf.Rad2Deg;
    }
}
