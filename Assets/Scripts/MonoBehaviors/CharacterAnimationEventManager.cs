using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Character)) ]
public class CharacterAnimationEventManager : MonoBehaviour
{
    Character parentChar;
    CharacterActions charActions;
    HitboxManager hitboxManager;

    [SerializeField]
    Projectile baseProjectilePrefab;

    private void Start()
    {
        parentChar = GetComponent<Character>();
        charActions = parentChar.charActions;
        hitboxManager = GetComponentInChildren<HitboxManager>();
    }

    public void Jump()
    {
        charActions.Jump();
    }

    public void AirJump()
    {
        charActions.AirJump();
    }

    public void WallJump()
    {
        charActions.WallJump();
    }

    public void CreateHitbox(HitboxData data)
    {
        hitboxManager.ActivateHitbox(data);
    }

    public void EnterLandingLag()
    {
        parentChar.charState.EnterLandingLag();
    }

    public void SetCurLandingLag(float duration)
    {
        parentChar.charState.curLandingLag = duration;
    }

    public void SetVelocityForward(float x)
    {
        if (parentChar.charState.isFacingRight)
        {
            parentChar.rb.velocity = new Vector2(x, parentChar.rb.velocity.y);
        }
        else
        {
            parentChar.rb.velocity = new Vector2(-x, parentChar.rb.velocity.y);
        }
    }

    public void SetVelocityY(float y)
    {
        parentChar.rb.velocity = new Vector2(parentChar.rb.velocity.x, y);
    }

    public void StallGravity()
    {
        parentChar.rb.gravityScale = 0;
    }

    public void ResetGravity()
    {
        parentChar.rb.gravityScale = parentChar.charSettings.gravityScale;
    }

    public void SetCanMoveHorizontal(int b)
    {
        if (b == 1)
        {
            parentChar.charState.canMoveHorizontal = true;
        }
        else if (b == 0)
        {
            parentChar.charState.canMoveHorizontal = false;
        }
        
    }

    public void CreateProjectile(ProjectileSettings projectileSettings)
    {
        baseProjectilePrefab.projectileSettings = projectileSettings;
        Projectile p;
        p= Instantiate(baseProjectilePrefab,parentChar.transform.position + (Vector3)projectileSettings.spawnOffset,parentChar.transform.rotation);
        p.parentChar = parentChar;
        
    }

    public void GainSuperArmor(float duration)
    {
        parentChar.charState.superArmorDuration = duration;
    }

    public void SetArmorValue(float value)
    {
        parentChar.charState.armor = value;
    }



}
