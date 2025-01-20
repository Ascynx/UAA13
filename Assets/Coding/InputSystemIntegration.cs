using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.U2D;

public class InputSystemIntegration : ScriptableObject
{
    private static PlayerControls controls;
    private static PlayerInput _playerInputInstance;

    public void SetPlayerInputInstance(PlayerInput playerInputInstance)
    {
        _playerInputInstance = playerInputInstance;
    }

    private void OnEnable()
    {
        controls = new PlayerControls();
    }

    public string GetControlScheme()
    {
        if (_playerInputInstance == null)
        {
            return "N/A";
        }
        return _playerInputInstance.currentControlScheme;
    }

    public string GetKeybindForAction(string action)
    {
        string controlScheme = GetControlScheme();
        InputBinding binding = InputBinding.MaskByGroup(controlScheme);
        InputAction map = _playerInputInstance.currentActionMap[action];
        string stuff = map.GetBindingDisplayString(binding);
        Debug.Log($"scheme: {controlScheme}, binding: {binding.name}, map: {map.name}, bound key: {stuff}");
        return stuff;
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
}
