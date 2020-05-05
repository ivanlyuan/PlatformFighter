using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    Hitbox[] Hitboxes;

	void Start ()
    {
        Hitboxes = GetComponentsInChildren<Hitbox>();
	}

    public void ActivateHitbox(HitboxData data)
    {
        foreach (Hitbox h in Hitboxes)
        {
            if (h.hitboxState == Hitbox.HitboxState.Closed)
            {
                h.ActivateHitbox(data);
                return;
            }
        }
    }



	
}
