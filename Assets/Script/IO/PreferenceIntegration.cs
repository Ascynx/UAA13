using UnityEngine;
using UnityEngine.InputSystem;
using static PreferenceValueFactory;

public class PreferenceIntegration : MonoBehaviour
{
    ValueHandle<string> _playerLayoutHandle;
    
    // Start is called before the first frame update
    void Start()
    {
        RegisterEventListeners();
        _playerLayoutHandle = CreateHandleOf("PlayerLayout", "");
        if (_playerLayoutHandle.GetValue() == null)
        {
            _playerLayoutHandle.SetValue(GetKeyboardDefaultLayout());
        }
    }

    private void RegisterEventListeners()
    {
        InputSystem.onDeviceChange += OnDeviceChanged;
    }

    private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.ConfigurationChanged)
        {
            Debug.Log("Changé de configuration pour un appareil, vérification du layout utilisé.");
            _playerLayoutHandle.SetValue(GetKeyboardDefaultLayout());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected string GetKeyboardDefaultLayout()
    {
        if (Keyboard.current == null)
        {
            return null;
        }
        return Keyboard.current.keyboardLayout;
    }
}
