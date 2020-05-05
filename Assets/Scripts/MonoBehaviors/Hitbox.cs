using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public enum HitboxState {Closed,Opened,Hitting }

    public HitboxData hitboxData;

    public ICanCreateHitbox hitboxCreator;
    public Character parentChar;

    [SerializeField]
    private HitboxState _hitboxState;
    public HitboxState hitboxState { get { return _hitboxState; } private set { _hitboxState = value; } }

    [SerializeField]
    private Collider2D[] HurtboxesHit;

    [SerializeField]
    private List<IGotHitResponder> ObjectsHit;

    private void Start ()
    {
        hitboxCreator = GetComponentInParent<ICanCreateHitbox>();
        parentChar = hitboxCreator.hitboxParentChar;
        ObjectsHit = new List<IGotHitResponder>();
	}

    private void OnDrawGizmos()
    {
        if (!hitboxData)
        {
            return;
        }

        switch (hitboxState)
        {
            case HitboxState.Closed:
                Gizmos.color = Color.gray;
                break;
            case HitboxState.Opened:
                Gizmos.color = Color.red;
                break;
            case HitboxState.Hitting:
                Gizmos.color = Color.magenta;
                break;
            default:
                break;
        }
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(new Vector2(hitboxData.positionOffset.x,hitboxData.positionOffset.y), new Vector2(hitboxData.size.x, hitboxData.size.y));
    }


    private void CheckForHit()
    {
        //Debug.Log("CheckForHit()");

        HurtboxesHit = Physics2D.OverlapBoxAll(transform.position + new Vector3(hitboxData.positionOffset.x * (parentChar.charState.isFacingRight ? -1 : 1), hitboxData.positionOffset.y, 0),
            hitboxData.size, hitboxData.rotation, 1 << LayerMask.NameToLayer("Hurtbox"));

        if (HurtboxesHit.Length > 0)
        {
            for (int i = 0; i < HurtboxesHit.Length; i++)//apply hit to hurtboxes
            {
                Collider2D c = HurtboxesHit[i];
                CollidedWith(c);
            }

            if (HasHitAtLeastOneEnemy())
            {
                hitboxState = HitboxState.Hitting;
            }
        }
        else
        {
            hitboxState = HitboxState.Opened;
        }

    }

    public void ActivateHitbox(HitboxData _hitboxData)
    {
        hitboxData = _hitboxData;
        ObjectsHit.Clear();
        StartCoroutine(IeActivateHitbox());
    }

    IEnumerator IeActivateHitbox()
    {
        hitboxState = HitboxState.Opened;

        for (int frame = 0; frame < hitboxData.numOfActiveFrames; frame++)
        {
            CheckForHit();
            yield return new WaitForFixedUpdate();
        }

        hitboxState = HitboxState.Closed;
    }

    public void CollidedWith(Collider2D collider)
    {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

        if (hurtbox)
        {
            if ((hurtbox.parentResponder.GetType() == typeof(Character)))
            {
                if ((Character)hurtbox.parentResponder == parentChar)//do not hit yourself
                {
                    return;
                }
            }
            foreach (IGotHitResponder o in ObjectsHit)
            {
                if (o == hurtbox.parentResponder)//can not hit the same object multiple times with the same hitbox
                {
                    return;
                }
            }

            hurtbox.GotHit(this);
            ObjectsHit.Add(hurtbox.parentResponder);
            parentChar.charState.OnDealDamage(hitboxData.damage);
            StartCoroutine(ApplyHitlag(FormulaHelper.GetFramesOfHitLag(hitboxData)));


        }
    }

    private IEnumerator ApplyHitlag(int frames)
    {
        //freeze yourself and parent of hurtbox
        Debug.Log("ApplyHitLag" + frames);
        Time.timeScale = 0.1f;
        for (int i = 0; i < frames; i++)
        {
            yield return new WaitForSecondsRealtime(0.02f);
        }
        Time.timeScale = 1f;
    }

    public bool HasHitAtLeastOneEnemy()
    {
        for (int i = 0; i < HurtboxesHit.Length; i++)//apply hit to hurtboxes
        {
            Collider2D c = HurtboxesHit[i];
            if (c.GetComponent<Hurtbox>().parentResponder.GetType() == typeof(Character))//hits a character
            {
                if ((Character)c.GetComponent<Hurtbox>().parentResponder != parentChar)//not yourself
                {
                    return true;
                }
            }
            else//hits a non character
            {
                return true;
            }
        }

        return false;
    }


}
