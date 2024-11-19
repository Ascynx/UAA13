using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputSystemIntegration
{
    public static KeyControl GetCorrespondingKeybind(string key)
    {
        if (Keyboard.current == null)
        {
            return null;
        }
        return Keyboard.current.FindKeyOnCurrentKeyboardLayout(key);
    }
}
