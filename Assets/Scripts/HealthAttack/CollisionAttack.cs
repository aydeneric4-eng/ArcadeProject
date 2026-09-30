using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionAttack : MonoBehaviour
{
    [SerializeField] float damage = 50f;
    [SerializeField] float attackKnockback = 10f;
    [SerializeField] float selfKnockback = 10f;
}
