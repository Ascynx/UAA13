using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemIntegration : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField]
    private PlayerInput _playerInputInstance;

    public PlayerInput PlayerInputInstance
    {
        get { return _playerInputInstance; }
    }

    private void OnEnable()
    {
        controls = new PlayerControls();
    }

    public string GetControlScheme()
    {
        return _playerInputInstance.currentControlScheme;
    }

    public string KeybindToSpriteKey(InputBinding binding)
    {
        string controlScheme = GetControlScheme();

        string groupPrefix = "";
        switch (controlScheme)
        {
            case "Clavier":
                {
                    groupPrefix = "Keyboard";
                    break;
                }
            case "Gamepad PS":
                {
                    groupPrefix = "P4";
                    break;
                }
            case "Gamepad X":
                {
                    groupPrefix = "X";
                    break;
                }
            case "Gamepad S":
                {
                    groupPrefix = "S";
                    break;
                }
        }
        return groupPrefix + "_" + DisplayKeyToSpriteKey(binding.ToDisplayString());
    }

    private static readonly string[] IGNORED_DETERMINANT = new string[]
    {
        "Numpad"
    };

    private string DisplayKeyToSpriteKey(string bindingDisplay)
    {
        string[] splits = bindingDisplay.Split(' ');
        StringBuilder builder = new StringBuilder();
        for (int i = splits.Length - 1; i >= 0; i--)
        {
            string split = splits[i];
            if (IGNORED_DETERMINANT.Contains(split))
            {
                continue;
            }

            builder.Append(split);
        }
        return builder.ToString();
    }

    public List<InputBinding> GetKeybindsForAction(string actionString)
    {
        string controlScheme = GetControlScheme();

        //les bindings peuvent être utilisé comme Query si on utilise les différents attributs comme Masques.
        InputBinding searchMask = new()
        {
            action = actionString,
            groups = controlScheme
        };


        InputAction action = _playerInputInstance.currentActionMap[actionString];
        List<InputBinding> matchingBindings = new();
        for (int i = 0; i < action.bindings.Count; i++)
        {
            InputBinding binding = action.bindings[i];
            if (searchMask.Matches(binding))
            {
                matchingBindings.Add(binding);
            }
        }

        return matchingBindings;
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
