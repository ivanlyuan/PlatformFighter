using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions
{
    private readonly ICharacterInput charInput;
    private Rigidbody2D rbToMove;
    private readonly CharacterSettings charSettings;
    private readonly CharacterState charState;
    private readonly CharacterAnimations charAnimations;
    private float targetXSpeed;
    private int bufferCounter = 0;//number of frames waited to execute an input;

    public CharacterActions
    (ICharacterInput _charInput,Rigidbody2D _rbToMove, CharacterSettings _charSettings,CharacterState _charState,CharacterAnimations _charAnimations)
    {
        charInput = _charInput;
        rbToMove = _rbToMove;
        charSettings = _charSettings;
        charState = _charState;
        charAnimations = _charAnimations;
    }

    public void Tick()
    {
        HandleActionInput();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (charState.hitstunDuration > 0 || !charState.canMoveHorizontal || charState.lagDuration > 0)
        {
            return;
        }



        if (charState.isGrounded)
        {
            targetXSpeed = charInput.InputDirectionX * charSettings.groundSpeed;
            rbToMove.velocity = new Vector2(targetXSpeed,rbToMove.velocity.y);
            if (charState.isFacingRight && charInput.InputDirectionX < 0)
            {
                charState.Flip();
            }
            else if (!charState.isFacingRight && charInput.InputDirectionX > 0)
            {
                charState.Flip();
            }
        }
        else // in the air
        {
            targetXSpeed = charInput.InputDirectionX * charSettings.airSpeed;

            if ((rbToMove.velocity.x == targetXSpeed))
            {
                return;
            }
            else
            {
                rbToMove.velocity = new Vector2(rbToMove.velocity.x + ((rbToMove.velocity.x < targetXSpeed) ? 1 : -1) * charSettings.airSpeed / 10, rbToMove.velocity.y);

            }
        }

    }


    private void HandleActionInput()
    {
        if (charInput.actionInputs.Count ==0)
        {
            return;
        }

        if (charState.hitstunDuration > 0 || !charAnimations.IsCurStateTag("Idle"))//can't do things when in lag or hitstun
        {
            bufferCounter++;
            if (bufferCounter > GlobalParameterHelper.INPUT_BUFFER_SIZE)
            {
                Debug.Log("Clear Buffer");
                charInput.actionInputs.Clear();
                charAnimations.ResetAllTriggers();
                bufferCounter = 0;
            }

            return;
        }

        //not enough meter to do a special
        if (charInput.actionInputs[0] == CharActionInput.EXNeutralSpecial ||
            charInput.actionInputs[0] == CharActionInput.EXUpSpecial ||
            charInput.actionInputs[0] == CharActionInput.EXDownSpecial ||
            charInput.actionInputs[0] == CharActionInput.EXLeftSpecial ||
            charInput.actionInputs[0] == CharActionInput.EXRightSpecial)
        {
            if (charState.meter < charSettings.sizeOfEachMeterBar)
            {
                charInput.actionInputs.RemoveAt(0);
                return;
            }
        }


        switch (charInput.actionInputs[0])//consume input and do things
        {
            case CharActionInput.Jump:
                if (!charState.isGrounded && charState.airJumpsRemaining <= 0 && !charState.backIsOnWall && !charState.frontIsOnWall) 
                {
                    charInput.actionInputs.RemoveAt(0);
                    return;
                }
                break;
            case CharActionInput.NeutralAttack:
                break;
            case CharActionInput.LeftAttack:
                break;
            case CharActionInput.RightAttack:
                break;
            case CharActionInput.UpAttack:
                break;
            case CharActionInput.DownAttack:
                break;
            case CharActionInput.NeutralSpecial:
                break;
            case CharActionInput.LeftSpecial:
                if (charState.isFacingRight)
                {
                    charState.Flip();
                }

                break;
            case CharActionInput.RightSpecial:
                if (!charState.isFacingRight)
                {
                    charState.Flip();
                }
                break;
            case CharActionInput.UpSpecial:
                break;
            case CharActionInput.DownSpecial:
                break;
            case CharActionInput.EXNeutralSpecial:
                charState.UseMeter(1);
                break;
            case CharActionInput.EXLeftSpecial:
                charState.UseMeter(1);
                if (charState.isFacingRight)
                {
                    charState.Flip();
                }
                break;
            case CharActionInput.EXRightSpecial:
                charState.UseMeter(1);
                if (!charState.isFacingRight)
                {
                    charState.Flip();
                }
                break;
            case CharActionInput.EXUpSpecial:
                charState.UseMeter(1);
                break;
            case CharActionInput.EXDownSpecial:
                charState.UseMeter(1);
                break;
            default:
                break;
        }

        bufferCounter = 0;
        TellCharAnimations(charInput.actionInputs[0]);
        //Debug.Log(charInput.actionInputs[0].ToString());
        charInput.actionInputs.RemoveAt(0);
    }

    public void Jump()
    {
        rbToMove.velocity = Vector2.zero;
        rbToMove.AddForce(new Vector2(charInput.InputDirectionX * charSettings.airSpeed * GlobalParameterHelper.JUMP_XAXIS_BOOST, charSettings.jumpPower));
    }

    public void AirJump()
    {
        if (charState.airJumpsRemaining > 0)
        {
            charState.OnAirJump();
            rbToMove.velocity = Vector2.zero;
            rbToMove.AddForce(new Vector2(charInput.InputDirectionX * charSettings.airSpeed * GlobalParameterHelper.JUMP_XAXIS_BOOST, charSettings.jumpPower));
        }
    }

    public void WallJump()
    {

        rbToMove.velocity = Vector2.zero;

        if (charState.frontIsOnWall)
        {
            Debug.Log("FrontWallJump");
            charState.Flip();
            rbToMove.AddForce(new Vector2((charState.isFacingRight ? 1 : -1) * charSettings.jumpPower/2, charSettings.jumpPower));
        }
        else if (charState.backIsOnWall)
        {
            Debug.Log("BackWallJump");
            rbToMove.AddForce(new Vector2((charState.isFacingRight ? 1 : -1) * charSettings.jumpPower/2, charSettings.jumpPower));
        }

    }

    private void TellCharAnimations(CharActionInput charActionInput)
    {
        charAnimations.HandleActionInput(charActionInput);
    }

}
