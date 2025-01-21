using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemIntegration : MonoBehaviour
{
    private PlayerControls controls;

    [SerializeField]
    private PlayerInput _playerInputInstance;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    public string GetControlScheme()
    {
        return _playerInputInstance.currentControlScheme;
    }

    public string GetKeybindForAction(string action)
    {
        string controlScheme = GetControlScheme();
        InputBinding binding = InputBinding.MaskByGroup(controlScheme);
        InputAction map = _playerInputInstance.currentActionMap[action];
        string boundKeys = map.GetBindingDisplayString(binding);

        Debug.Log(binding.effectivePath);

        if (binding.isComposite)
        {
            Debug.Log("Composite keybind");
            //il a probablement laché, on récupère toutes les "sous"-bindings et on les concatènes.
            boundKeys = binding.GetNameOfComposite();
        }

        Debug.Log($"scheme: {controlScheme}, binding: {binding.name}, map: {map.name}, bound key: {boundKeys}");
        return boundKeys;
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
