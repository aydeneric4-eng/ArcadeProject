using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitParentAndAimAtTarget : MonoBehaviour
{
    [SerializeField] bool isTargetMouse = false;
    [SerializeField] Transform targetTransform;
    [SerializeField] bool rotateTowardsTarget;

    [SerializeField] Transform orbitParentTransform;
    [SerializeField] bool orbitTowardsTarget;
    [SerializeField] float orbitDistance = 0.1f;

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
            if (Mouse.current != null)
            { //Ai
                Vector2 screenPosition = Mouse.current.position.ReadValue();
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                worldPosition.z = 0f; //Ai
                targetPosition = worldPosition;
            }
        }
        else
        {
            targetPosition = (Vector2)targetTransform.position;
        }
        Vector2 selfPos = (Vector2)selfTransform.position;
        float angleToTarget = CustomUtilities.GetAngleOf2DVect(orbitParentTransform.position, targetPosition);

        if (rotateTowardsTarget)
        {
            transform.eulerAngles = new Vector3(0,0,angleToTarget);
        }
        if (orbitTowardsTarget)
        {
            selfTransform.position = orbitParentTransform.position + new Vector3(Mathf.Cos(angleToTarget * Mathf.Deg2Rad) * orbitDistance, Mathf.Sin(angleToTarget*Mathf.Deg2Rad)*orbitDistance, 0);
        }

    }
}
