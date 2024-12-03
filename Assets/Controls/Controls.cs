//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Controls/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""OpenWorld"",
            ""id"": ""8fbf3376-4c62-4c4d-8d60-142bda2b136a"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Value"",
                    ""id"": ""abe17551-4265-4f35-8fc4-b980a7015e98"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Value"",
                    ""id"": ""8e5bac51-91cf-4eae-9d19-d47274fe7d5e"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""18655375-c41e-4b09-80dc-f1612e7f1614"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""90f53427-e3ac-4642-97c1-424098b9e539"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (WASD)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f092393a-5c25-4f62-b17f-39d5ec5b0a3e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (WASD)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c32cee22-9c24-4373-97e7-9348b30e43b9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (WASD)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d1370210-05b5-4e0c-ae39-8e2eb53fdffd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (WASD)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""600da452-26e8-463f-a7d9-0363137d1374"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bdc4ea57-7549-4055-9e98-36f5ea847b61"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (Fleches)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""53175df1-bb58-4fe2-a7cb-27b6ebff4154"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (Fleches)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""709727e6-56ea-49fd-82f7-c77f262938cd"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (Fleches)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""28f462c7-f9f8-4f7c-9fd9-f782579f17cb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (Fleches)"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d4d14d4e-3344-4389-9d2c-f06a5ba2d78c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdcac53c-bc02-48d7-b9b5-83e154cc5738"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (Fleches)"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73c202b3-a386-493a-b324-33a1d05ecc67"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Clavier (WASD)"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa7d360b-f7c8-4cdc-9beb-6699e8d6f9ee"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Clavier (WASD)"",
            ""bindingGroup"": ""Clavier (WASD)"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Clavier (Fleches)"",
            ""bindingGroup"": ""Clavier (Fleches)"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // OpenWorld
        m_OpenWorld = asset.FindActionMap("OpenWorld", throwIfNotFound: true);
        m_OpenWorld_Direction = m_OpenWorld.FindAction("Direction", throwIfNotFound: true);
        m_OpenWorld_Sprint = m_OpenWorld.FindAction("Sprint", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // OpenWorld
    private readonly InputActionMap m_OpenWorld;
    private List<IOpenWorldActions> m_OpenWorldActionsCallbackInterfaces = new List<IOpenWorldActions>();
    private readonly InputAction m_OpenWorld_Direction;
    private readonly InputAction m_OpenWorld_Sprint;
    public struct OpenWorldActions
    {
        private @PlayerControls m_Wrapper;
        public OpenWorldActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Direction => m_Wrapper.m_OpenWorld_Direction;
        public InputAction @Sprint => m_Wrapper.m_OpenWorld_Sprint;
        public InputActionMap Get() { return m_Wrapper.m_OpenWorld; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OpenWorldActions set) { return set.Get(); }
        public void AddCallbacks(IOpenWorldActions instance)
        {
            if (instance == null || m_Wrapper.m_OpenWorldActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_OpenWorldActionsCallbackInterfaces.Add(instance);
            @Direction.started += instance.OnDirection;
            @Direction.performed += instance.OnDirection;
            @Direction.canceled += instance.OnDirection;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
        }

        private void UnregisterCallbacks(IOpenWorldActions instance)
        {
            @Direction.started -= instance.OnDirection;
            @Direction.performed -= instance.OnDirection;
            @Direction.canceled -= instance.OnDirection;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
        }

        public void RemoveCallbacks(IOpenWorldActions instance)
        {
            if (m_Wrapper.m_OpenWorldActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IOpenWorldActions instance)
        {
            foreach (var item in m_Wrapper.m_OpenWorldActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_OpenWorldActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public OpenWorldActions @OpenWorld => new OpenWorldActions(this);
    private int m_ClavierWASDSchemeIndex = -1;
    public InputControlScheme ClavierWASDScheme
    {
        get
        {
            if (m_ClavierWASDSchemeIndex == -1) m_ClavierWASDSchemeIndex = asset.FindControlSchemeIndex("Clavier (WASD)");
            return asset.controlSchemes[m_ClavierWASDSchemeIndex];
        }
    }
    private int m_ClavierFlechesSchemeIndex = -1;
    public InputControlScheme ClavierFlechesScheme
    {
        get
        {
            if (m_ClavierFlechesSchemeIndex == -1) m_ClavierFlechesSchemeIndex = asset.FindControlSchemeIndex("Clavier (Fleches)");
            return asset.controlSchemes[m_ClavierFlechesSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IOpenWorldActions
    {
        void OnDirection(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
    }
}