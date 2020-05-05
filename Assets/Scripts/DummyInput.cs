using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyInput : ICharacterInput
{
    public float InputDirectionX { get; private set; }

    public List<CharActionInput> actionInputs { get; private set; }

    public DummyInput()
    {
        actionInputs = new List<CharActionInput>();
    }

    public void ReadInput()
    {
        return;
    }
}
