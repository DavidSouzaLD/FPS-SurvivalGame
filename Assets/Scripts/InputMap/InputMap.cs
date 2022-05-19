//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/InputMap/InputMap.inputactions
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

public partial class @InputMap : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9ebe6139-c13f-45f7-b5bd-58e45e8dbd3f"",
            ""actions"": [
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Button"",
                    ""id"": ""d3771e79-b803-4473-8777-4e8ea6f23647"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FireTap1"",
                    ""type"": ""Button"",
                    ""id"": ""e24c99e5-b56e-4ce7-8a02-89c101dd3dad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire2"",
                    ""type"": ""Button"",
                    ""id"": ""b572137f-5aea-4ffb-8b96-d0b1e97821ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FireTap2"",
                    ""type"": ""Button"",
                    ""id"": ""84e7c2bd-9f5b-4f74-9957-7a754f0d03f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""KeyAxis"",
                    ""type"": ""Value"",
                    ""id"": ""7a200e75-07f9-46f7-9797-da00e39350b0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseAxis"",
                    ""type"": ""Value"",
                    ""id"": ""4bf95b51-e6ae-4351-8cec-8b1cba1f3ab2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""364a35c8-2221-473f-8b20-dd41b1435370"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""92e29235-0935-4bf8-8772-d474a5cc8c97"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""6131f638-7d66-43d6-a09d-a2ee2c140988"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""74f1592b-9fa6-41a1-b84c-a7af56a128fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""81a48282-7807-46ab-97d3-7a93b541c058"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""aa19dbbd-5a95-4cf9-914d-b6e5cb9c0690"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""faba35ec-fe66-4cb6-b6aa-c572cb9b9773"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a9d3ff4-54fa-4d3e-927d-6b94619c6a7b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59863600-ab9e-408b-a1ba-4791fa06c1a8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e25676c-b4e3-483e-9a1e-0b046c9d1e64"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0dff8afa-a9eb-423f-a82a-adb32c266d38"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""98ea5273-a1e8-44f2-836b-451decbae28e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f8248976-a5f6-4821-b2a6-9c0580ad4685"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""72bdd675-7bfb-474a-be90-e051419d861e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""be524a61-1474-42c4-9ef9-f4ee3673d681"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""838c8937-c098-4bb3-8b7e-44936261e6ff"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d08df224-e13d-4f1c-9e5a-e6298f1896ad"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireTap1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f69e4c3c-53e4-4eab-87d7-69f5949ff5bb"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9011b80-5a10-4246-9965-8e5b554729f8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireTap2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""765ff4f0-f9f0-43ba-86fc-f6d73ecb067d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a01663b5-acd1-43a4-9361-053502775de2"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47c3e754-8040-4e34-b20c-d86dda3a9e5e"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Fire1 = m_Player.FindAction("Fire1", throwIfNotFound: true);
        m_Player_FireTap1 = m_Player.FindAction("FireTap1", throwIfNotFound: true);
        m_Player_Fire2 = m_Player.FindAction("Fire2", throwIfNotFound: true);
        m_Player_FireTap2 = m_Player.FindAction("FireTap2", throwIfNotFound: true);
        m_Player_KeyAxis = m_Player.FindAction("KeyAxis", throwIfNotFound: true);
        m_Player_MouseAxis = m_Player.FindAction("MouseAxis", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_ESC = m_Player.FindAction("ESC", throwIfNotFound: true);
        m_Player_Zoom = m_Player.FindAction("Zoom", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Fire1;
    private readonly InputAction m_Player_FireTap1;
    private readonly InputAction m_Player_Fire2;
    private readonly InputAction m_Player_FireTap2;
    private readonly InputAction m_Player_KeyAxis;
    private readonly InputAction m_Player_MouseAxis;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_ESC;
    private readonly InputAction m_Player_Zoom;
    public struct PlayerActions
    {
        private @InputMap m_Wrapper;
        public PlayerActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire1 => m_Wrapper.m_Player_Fire1;
        public InputAction @FireTap1 => m_Wrapper.m_Player_FireTap1;
        public InputAction @Fire2 => m_Wrapper.m_Player_Fire2;
        public InputAction @FireTap2 => m_Wrapper.m_Player_FireTap2;
        public InputAction @KeyAxis => m_Wrapper.m_Player_KeyAxis;
        public InputAction @MouseAxis => m_Wrapper.m_Player_MouseAxis;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @ESC => m_Wrapper.m_Player_ESC;
        public InputAction @Zoom => m_Wrapper.m_Player_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Fire1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @Fire1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire1;
                @FireTap1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireTap1;
                @FireTap1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireTap1;
                @FireTap1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireTap1;
                @Fire2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire2;
                @Fire2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire2;
                @Fire2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire2;
                @FireTap2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireTap2;
                @FireTap2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireTap2;
                @FireTap2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireTap2;
                @KeyAxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyAxis;
                @KeyAxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyAxis;
                @KeyAxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnKeyAxis;
                @MouseAxis.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseAxis;
                @MouseAxis.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseAxis;
                @MouseAxis.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseAxis;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @ESC.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnESC;
                @Zoom.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Fire1.started += instance.OnFire1;
                @Fire1.performed += instance.OnFire1;
                @Fire1.canceled += instance.OnFire1;
                @FireTap1.started += instance.OnFireTap1;
                @FireTap1.performed += instance.OnFireTap1;
                @FireTap1.canceled += instance.OnFireTap1;
                @Fire2.started += instance.OnFire2;
                @Fire2.performed += instance.OnFire2;
                @Fire2.canceled += instance.OnFire2;
                @FireTap2.started += instance.OnFireTap2;
                @FireTap2.performed += instance.OnFireTap2;
                @FireTap2.canceled += instance.OnFireTap2;
                @KeyAxis.started += instance.OnKeyAxis;
                @KeyAxis.performed += instance.OnKeyAxis;
                @KeyAxis.canceled += instance.OnKeyAxis;
                @MouseAxis.started += instance.OnMouseAxis;
                @MouseAxis.performed += instance.OnMouseAxis;
                @MouseAxis.canceled += instance.OnMouseAxis;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnFire1(InputAction.CallbackContext context);
        void OnFireTap1(InputAction.CallbackContext context);
        void OnFire2(InputAction.CallbackContext context);
        void OnFireTap2(InputAction.CallbackContext context);
        void OnKeyAxis(InputAction.CallbackContext context);
        void OnMouseAxis(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnESC(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
