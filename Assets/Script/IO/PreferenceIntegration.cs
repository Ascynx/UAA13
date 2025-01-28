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
            Debug.Log("Chang� de configuration pour un appareil, v�rification du layout utilis�.");
            _playerLayoutHandle.SetValue(GetControlScheme());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected string GetControlScheme()
    {
        return Jeu.Instance.inputIntegration.PlayerInputInstance.currentControlScheme;
    }
}
