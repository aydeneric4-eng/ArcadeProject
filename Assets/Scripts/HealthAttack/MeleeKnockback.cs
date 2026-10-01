using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MeleeKnockback : MonoBehaviour
{
    [SerializeField] float knockbackPower = 50f;
    [SerializeField] float selfKnockbackPower = 50f;

    private RBMovement selfRBMovement;

    private void Awake()
    {
        selfRBMovement = GetComponent<RBMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.TryGetComponent<RBMovement>(out RBMovement colliderRBMovement))
        {
            //colliderRBMovement.AddImpulse();
        }
    }
}
