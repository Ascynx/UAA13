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

    public Optional<string> KeybindToSpriteKey(InputBinding binding)
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
        return Optional<string>.Of(groupPrefix + "_" + DisplayKeyToSpriteKey(binding.ToDisplayString()));
    }

    private static readonly string[] IGNORED_DETERMINANT = new string[]
    {
        "Numpad"
    };

    private string DisplayKeyToSpriteKey(string bindingDisplay)
    {
        string[] splits = bindingDisplay.Split(' ');
        StringBuilder builder = new();
        for (int i = splits.Length - 1; i >= 0; i--)
        {
            string split = splits[i];
            if (IGNORED_DETERMINANT.Contains(split))
            {
                continue;
            }

            if (split.Length > 2 && split.StartsWith("\"") && split.EndsWith("\""))
            {
                split = split[1..^1];
            }

            builder.Append(split);
        }
        return builder.ToString();
    }

    public List<InputBinding> GetKeybindsForAction(string actionString)
    {
        string controlScheme = GetControlScheme();

        //les bindings peuvent �tre utilis� comme Query si on utilise les diff�rents attributs comme Masques.
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

    public void SwitchToGuiMap(out string oldMap)
    {
        SetActionMap("gui", out oldMap);
    }

    public void SwitchToOverworldMap(out string oldMap)
    {
        SetActionMap("OpenWorld", out oldMap);
    }

    /// <summary>
    /// Permet de changer l'actionMap utilis�e.
    /// </summary>
    /// <param name="actionMap">le nom de la map que l'on veut avoir actif</param>
    /// <param name="oldMap">Le nom de la map qui �tait active.</param>
    /// <returns>Si on a pu changer la map.</returns>
    public bool SetActionMap(string actionMap, out string oldMap)
    {
        oldMap = actionMap;
        if (_playerInputInstance.currentActionMap.name == actionMap)
        {
            return false;
        }
        _playerInputInstance.SwitchCurrentActionMap(actionMap);
        return true;
    }

    public string GetCurrentActionMap()
    {
        return _playerInputInstance.currentActionMap.name;
    }

    /// <summary>
    /// Permet de changer l'�tat d'une action en particulier
    /// </summary>
    /// <param name="state">le nouvel �tat de l'action</param>
    /// <param name="action">l'identifier de l'action</param>
    /// <param name="previousState">l'�tat avant le changement (donne false si l'action n'as pas �t� possible.)</param>
    /// <returns>Si l'�tat de l'action a bien �t� chang�</returns>
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
