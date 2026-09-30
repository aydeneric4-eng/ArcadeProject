using UnityEngine;

public class ConstantSpin : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;
    private Transform selfTransform;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        selfTransform.eulerAngles += new Vector3(0, 0, rotationSpeed * Time.fixedDeltaTime);
    }
}
