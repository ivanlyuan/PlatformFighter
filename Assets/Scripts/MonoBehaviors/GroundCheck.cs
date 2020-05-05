using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundCheck : MonoBehaviour
{

    private bool _isGrounded;
    public bool isGrounded { get { return _isGrounded; } set { _isGrounded = value; TellChar(); } }

    private Character parentChar;


    private void Start()
    {
        parentChar = GetComponentInParent<Character>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.enabled)
            {
                isGrounded = true;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (collision.enabled)
            {
                isGrounded = false;
            }
        }
    }

    private void TellChar()
    {
        if (isGrounded)
        {
            parentChar.OnLand();
        }
        else
        {
            parentChar.OnLeaveGround();
        }

    }


}
