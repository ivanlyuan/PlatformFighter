using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Collider2D))]
public class WallCheck : MonoBehaviour // merge with GroundCheck later
{

    [SerializeField]
    bool isFront;

    [SerializeField]
    private bool _isOnWall;
    public bool isOnWall { get { return _isOnWall; } set { _isOnWall = value; TellChar(isFront); } }

    private Character parentChar;
    private Collider2D col2D;

    void Start ()
    {
        parentChar = GetComponentInParent<Character>();
        col2D = GetComponent<Collider2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.enabled)
            {
                isOnWall = true;
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.enabled)
            {
                isOnWall = false;
            }
        }
    }

    private void TellChar(bool isFront)
    {
        if (!parentChar)
        {
            return;
        }

        if (isOnWall)
        {
            parentChar.OnTouchWall(isFront);
        }
        else
        {
            parentChar.OnLeaveWall(isFront);
        }

    }

}


