using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using static InputIconsManager;

public class InputSystemIntegration : ScriptableObject
{
    private static PlayerControls controls;
    private static InputIconsManager iconsManager;

    private void Awake()
    {
        controls = new PlayerControls();
        iconsManager = ScriptableObject.CreateInstance<InputIconsManager>();
    }

    /// <summary>
    /// Permet de changer l'état d'une action en particulier
    /// </summary>
    /// <param name="state">le nouvel état de l'action</param>
    /// <param name="action">l'identifier de l'action</param>
    /// <param name="previousState">l'état avant le changement (donne false si l'action n'as pas été possible.)</param>
    /// <returns>Si l'état de l'action a bien été changé</returns>
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

    public InputIconsManager GetIconsManager()
    {
        return iconsManager;
    }
}
