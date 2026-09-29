using UnityEngine;

public class CircularAimAtTarget : MonoBehaviour
{
    [SerializeField] bool isTargetMouse;
    [SerializeField] Vector2 targetTransform;
    [SerializeField] bool rotateTowardsTarget;
    [SerializeField] bool moveTowardsTarget;
    [SerializeField] float maxMoveDistance;
    [SerializeField] float minMoveDistance;
}
