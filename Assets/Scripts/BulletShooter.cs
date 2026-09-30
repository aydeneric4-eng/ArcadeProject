using System;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Bullet bulletPrefab;

    [SerializeField] float rateOfFire = 1f;
    private float lastFiredTime = -999f;

    public void ShootBullet()
    {
        if (!CustomUtilities.HasTimeElapsed(lastFiredTime,rateOfFire))
        {
            return;
        }

        lastFiredTime = Time.time;
        Bullet newBullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
    }
}
