using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ICharacterInput
{
    public float InputDirectionX { get; private set; }
    public int playerNumber;

    public List<CharActionInput> actionInputs { get; private set; }

    public PlayerInput(int _playerNumber)
    {
        playerNumber = _playerNumber;
        actionInputs = new List<CharActionInput>();
    }



    public void ReadInput()
    {
        InputDirectionX = Input.GetAxisRaw("Horizontal" + playerNumber);

        if (Input.GetKeyDown(KeyCode.Z))//jump or air jump
        {
            CharActionInput input = CharActionInput.Jump;
            actionInputs.Add(input);
        }


        if (Input.GetKeyDown(KeyCode.X))//normal attacks
        {
            CharActionInput input = CharActionInput.NeutralAttack;
            if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)//up
            {
                input = CharActionInput.UpAttack;
            }
            else if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)//down
            {
                input = CharActionInput.DownAttack;
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)//left
            {
                input = CharActionInput.LeftAttack;
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)//right
            {
                input = CharActionInput.RightAttack;
            }

            actionInputs.Add(input);

        }
        
        if (Input.GetKeyDown(KeyCode.C))//special attacks
        {
            Debug.Log("Disabled special attacks because they aren't finished yet!");
            /*
            CharActionInput input = CharActionInput.NeutralSpecial;
            if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)//up
            {
                input = CharActionInput.UpSpecial;
            }
            else if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)//down
            {
                input = CharActionInput.DownSpecial;
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)//left
            {
                input = CharActionInput.LeftSpecial;
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)//right
            {
                input = CharActionInput.RightSpecial;
            }

            actionInputs.Add(input);
            */
        }

        if (Input.GetKeyDown(KeyCode.V))// EX special attacks
        {
            Debug.Log("Disabled special attacks because they aren't finished yet!");
            /*
            CharActionInput input = CharActionInput.EXNeutralSpecial;
            if (Input.GetAxisRaw("Vertical" + playerNumber) > 0)//up
            {
                input = CharActionInput.EXUpSpecial;
            }
            else if (Input.GetAxisRaw("Vertical" + playerNumber) < 0)//down
            {
                input = CharActionInput.EXDownSpecial;
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNumber) < 0)//left
            {
                input = CharActionInput.EXLeftSpecial;
            }
            else if (Input.GetAxisRaw("Horizontal" + playerNumber) > 0)//right
            {
                input = CharActionInput.EXRightSpecial;
            }

            actionInputs.Add(input);
            */
        }
    }
}
