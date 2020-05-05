using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : IGotHitResponder
{
    private readonly Character parentChar;
    private readonly CharacterState charState;

    public readonly Animator animator;


    public CharacterAnimations(Character _parentChar, CharacterState _charState, Animator _animator)
    {
        parentChar = _parentChar;
        charState = _charState;
        animator = _animator;
    }

    public void HandleActionInput(CharActionInput actionInput)
    {
        string s = actionInput.ToString();


        animator.SetTrigger(s);
        Debug.Log(s);

    }

    public void OnGotHit(Hitbox hitbox)
    {

    }

    public void OnEnterHitstun()
    {
        animator.SetBool("isInHitstun", true);
    }

    public void OnExitHitstun()
    {
        animator.SetBool("isInHitstun", false);
    }

    public void SetIsGrounded(bool v)
    {
        animator.SetBool("isGrounded", v);
    }

    public void SetIsOnWall(bool v, bool isFront)
    {
        if (isFront)
        {
            animator.SetBool("frontIsOnWall", v);
        }
        else
        {
            animator.SetBool("backIsOnWall", v);
        }

    }

    public void SetIsFacingRight(bool v)
    {
        animator.SetBool("isFacingRight", v);
    }

    public void SetAirJumpsRemaining(int v)
    {
        animator.SetInteger("airJumpsRemaining", v);
    }

    public bool IsCurStateTag(string s)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsTag(s);
    }

    public void ResetAllTriggers()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(parameter.name);
            }

        }
    }

}
