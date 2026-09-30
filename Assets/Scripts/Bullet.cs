using UnityEngine;
using static CustomUtilities;

public class Bullet : MonoBehaviour, IHasTeam
{
    [SerializeField] PlayerTeams playerTeam = PlayerTeams.Player;
    public PlayerTeams PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }

    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float damage = 50f;

    private Transform selfTransform;

    private void Awake()
    {
        selfTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitData = Physics2D.Linecast(selfTransform.position, selfTransform.position + selfTransform.TransformDirection(new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0)));
        if (hitData)
        {
            handleCollision(hitData.transform.gameObject);
        }

        selfTransform.position += selfTransform.TransformDirection(new Vector3(moveSpeed * Time.fixedDeltaTime, 0, 0));
    }

    private void handleCollision(GameObject collidedObject)
    {
        if (collidedObject.TryGetComponent<IDamagable>(out IDamagable damageHandler))
        {
            if (damageHandler is IHasTeam team && team.PlayerTeam == playerTeam)
            {
                return;
            }
            damageHandler.Damage(damage);
        }
        Destroy(gameObject);
        Destroy(this);
    }
}
