using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox hurtbox = collision.GetComponent<Hurtbox>();
        if (hurtbox)
        {
            if (hurtbox.parentResponder.GetType() == typeof(Character))
            {
                UIManager.Instance.OnCharacterKO((Character)hurtbox.parentResponder);
                CharacterManager.Instance.OnCharacterKO((Character)hurtbox.parentResponder);
            }
        }

    }

}
