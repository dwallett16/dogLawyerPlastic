using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IInputManager
{
    public float GetAxisRaw(string input)
    {
        return Input.GetAxisRaw(input);
        
    }

    public bool GetButtonDown(string input)
    {
        return Input.GetButtonDown(input);
    }
}
