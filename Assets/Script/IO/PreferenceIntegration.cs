using Unity.VisualScripting;
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
            _playerLayoutHandle.SetValue(GetControlScheme());
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
            _playerLayoutHandle.SetValue(GetControlScheme());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ValueHandle<bool> GetBoolPreferenceHandle(string name, bool def)
    {
        ValueHandle<bool> handle = PreferenceValueFactory.CreateHandleOf(name, def);
        return handle;
    }

    public bool GetBoolPreference(string name, bool def)
    {
        return GetBoolPreferenceHandle(name, def).GetValue();
    }

    protected string GetControlScheme()
    {
        return Jeu.Instance.inputIntegration.PlayerInputInstance.currentControlScheme;
    }
}
