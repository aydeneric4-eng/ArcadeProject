using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.XR.Haptics;

[RequireComponent(typeof(Collider2D))]
public class MeleeKnockback : MonoBehaviour
{
    [SerializeField] float knockbackPower = 50f;
    [SerializeField] float selfKnockbackPower = 50f;
    [SerializeField] bool allwaysGiveSelfKnockback = false;

    private Transform selfTransform;
    private RBMovement selfRBMovement;

    private void Awake()
    {
        selfRBMovement = GetComponent<RBMovement>();
        selfTransform = GetComponent<Transform>();
    }

    private Vector2 collisionAverageNormal;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool colliderHasRBMovement = collision.collider.gameObject.TryGetComponent<RBMovement>(out RBMovement colliderRBMovement);

        if (!colliderHasRBMovement && !(selfRBMovement != null && (colliderHasRBMovement || allwaysGiveSelfKnockback)))
            return;

        collisionAverageNormal = Vector2.zero;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            collisionAverageNormal += contact.normal;
        }
        collisionAverageNormal = collisionAverageNormal / collision.contactCount;

        if (colliderHasRBMovement)
        {
            colliderRBMovement.ReceiveKnockback(-collisionAverageNormal * knockbackPower);
        }
        if (selfRBMovement != null && (colliderHasRBMovement || allwaysGiveSelfKnockback))
        {
            selfRBMovement.ReceiveKnockback(collisionAverageNormal * selfKnockbackPower);
        }
    }
}
