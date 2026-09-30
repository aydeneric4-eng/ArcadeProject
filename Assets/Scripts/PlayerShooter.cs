using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BulletShooter))]
public class PlayerShooter : MonoBehaviour
{
    BulletShooter bulletShooter;

    private void Awake()
    {
        bulletShooter = GetComponent<BulletShooter>();
    }

    public void ShootBullet(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.performed);
        bulletShooter.ShootBullet();
        //Debug.Log("Input shot");
    }
}
