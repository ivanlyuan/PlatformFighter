using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour,ICanCreateHitbox
{
    public ProjectileSettings projectileSettings;


    public Character parentChar;
    private HitboxManager hitboxManager;

    private Rigidbody2D rb;
    private Animator animator;


    private float curExistTime = 0.0f;
    private float maxExistTime;

    private void Start()
    {
        hitboxManager = GetComponent<HitboxManager>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = projectileSettings.gravityScale;
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = projectileSettings.animatorController;
        maxExistTime = projectileSettings.maxExistTime;

    }

    private void FixedUpdate()
    {
        curExistTime += Time.deltaTime;
        if (curExistTime >= maxExistTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
    }




    Character ICanCreateHitbox.hitboxParentChar
    {
        get
        {
            return parentChar;
        }
    }



}
