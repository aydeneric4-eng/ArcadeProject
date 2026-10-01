using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.XR.Haptics;

[RequireComponent(typeof(Collider2D))]
public class MeleeKnockback : MonoBehaviour, IGivesKnockback
{
    [SerializeField] float knockbackPower = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool colliderCanReceiveKnockback = collision.collider.gameObject.TryGetComponent<IReceivesKnockback>(out IReceivesKnockback colliderKBReceiver);

        if (!colliderCanReceiveKnockback)
            return;

        Vector2 collisionAverageNormal = CustomUtilities.GetAverageCollisionNormal(collision);
        colliderKBReceiver.ReceiveKnockback(-collisionAverageNormal * knockbackPower);
    }
}
