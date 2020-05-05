using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Projectile))]
public class ProjectileAnimationEventManager : MonoBehaviour
{
    Projectile parentProjectile;
    HitboxManager hitboxManager;



    private void Start()
    {
        parentProjectile = GetComponent<Projectile>();
        hitboxManager = GetComponentInChildren<HitboxManager>();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void CreateHitbox(HitboxData data)
    {
        hitboxManager.ActivateHitbox(data);
    }

    public void SetupVelocity()
    {
        parentProjectile.SetVelocity(parentProjectile.projectileSettings.launchAngle.x * (parentProjectile.parentChar.charState.isFacingRight ? 1 : -1), parentProjectile.projectileSettings.launchAngle.y);
    }


}
