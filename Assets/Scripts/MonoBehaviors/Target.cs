using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour,IGotHitResponder
{
    [SerializeField]
    private float hp;

    public int pointsWorth { get; private set; }


    void Start()
    {

    }

    private void Break(Hitbox hitbox)
    {
        UIManager.Instance.ChangeScore(10 + (int)hitbox.hitboxData.damage);
        //play break animation
        Destroy(gameObject);
    }

    public void OnGotHit(Hitbox hitbox)
    {
        hp -= hitbox.hitboxData.damage;

        if (hp <= 0)
        {
            Break(hitbox);
        }
    }
}
