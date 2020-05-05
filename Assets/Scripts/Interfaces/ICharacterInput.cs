using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInput
{
    void ReadInput();
    float InputDirectionX { get; }
    List<CharActionInput> actionInputs { get; }



}

public enum CharActionInput
{
    Jump,
    NeutralAttack,
    LeftAttack,
    RightAttack,
    UpAttack,
    DownAttack,
    NeutralSpecial,
    LeftSpecial,
    RightSpecial,
    UpSpecial,
    DownSpecial,
    EXNeutralSpecial,
    EXLeftSpecial,
    EXRightSpecial,
    EXUpSpecial,
    EXDownSpecial,
}




