using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;

    private Transform selfTransform;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        selfTransform.position += new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0);
    }

}
