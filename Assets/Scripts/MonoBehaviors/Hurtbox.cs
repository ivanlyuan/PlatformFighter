using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{


    public IGotHitResponder parentResponder { get; private set; }

    void Start()
    {
        parentResponder = GetComponentInParent<IGotHitResponder>();
    }

    public void GotHit(Hitbox hitbox)
    {
        parentResponder.OnGotHit(hitbox);
    }
}
