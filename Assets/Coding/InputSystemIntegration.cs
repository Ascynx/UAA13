using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class InputSystemIntegration : ScriptableObject
{
    private static PlayerControls controls;
    private static InputIconsManager iconsManager;

    private void Awake()
    {
        controls = new PlayerControls();
        iconsManager = ScriptableObject.CreateInstance<InputIconsManager>();
    }

    public bool SetActionState(bool state, string action, out bool previousState)
    {
        if (controls.FindAction(action) is InputAction inputAction)
        {
            previousState = inputAction.enabled;
            if (state == previousState)
            {
                return false;
            }

            if (state)
            {
                inputAction.Enable();
            } else
            {
                inputAction.Disable();
            }

            return true;
        }

        previousState = false;
        return false;
    }

    public void DisableAction(string action)
    {
        if (controls.FindAction(action) != null)
        {
            InputAction inputAction = controls.FindAction(action);
            if (!inputAction.enabled)
            {
                return;
            }
            inputAction.Disable();
        }
    }
    public void EnableAction(string action)
    {
        if (controls.FindAction(action) != null)
        {
            InputAction inputAction = controls.FindAction(action);
            if (inputAction.enabled)
            {
                return;
            }

            inputAction.Enable();
        }
    }
}
