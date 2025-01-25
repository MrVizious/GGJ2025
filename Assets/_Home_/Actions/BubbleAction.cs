//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/_Home_/Actions/BubbleAction.inputactions
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

public partial class @BubbleAction: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @BubbleAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""BubbleAction"",
    ""maps"": [
        {
            ""name"": ""BubbleMovement"",
            ""id"": ""cb98e304-6d44-4e27-89ba-deadfdbd8664"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d2baba6f-a76b-4eb2-94b0-a9fa19ebb625"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c742243d-8233-439f-b124-f080ed33817f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";XboxController"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""XboxController"",
            ""bindingGroup"": ""XboxController"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // BubbleMovement
        m_BubbleMovement = asset.FindActionMap("BubbleMovement", throwIfNotFound: true);
        m_BubbleMovement_Move = m_BubbleMovement.FindAction("Move", throwIfNotFound: true);
    }

    ~@BubbleAction()
    {
        UnityEngine.Debug.Assert(!m_BubbleMovement.enabled, "This will cause a leak and performance issues, BubbleAction.BubbleMovement.Disable() has not been called.");
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

    // BubbleMovement
    private readonly InputActionMap m_BubbleMovement;
    private List<IBubbleMovementActions> m_BubbleMovementActionsCallbackInterfaces = new List<IBubbleMovementActions>();
    private readonly InputAction m_BubbleMovement_Move;
    public struct BubbleMovementActions
    {
        private @BubbleAction m_Wrapper;
        public BubbleMovementActions(@BubbleAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_BubbleMovement_Move;
        public InputActionMap Get() { return m_Wrapper.m_BubbleMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BubbleMovementActions set) { return set.Get(); }
        public void AddCallbacks(IBubbleMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_BubbleMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BubbleMovementActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
        }

        private void UnregisterCallbacks(IBubbleMovementActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
        }

        public void RemoveCallbacks(IBubbleMovementActions instance)
        {
            if (m_Wrapper.m_BubbleMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBubbleMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_BubbleMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BubbleMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BubbleMovementActions @BubbleMovement => new BubbleMovementActions(this);
    private int m_XboxControllerSchemeIndex = -1;
    public InputControlScheme XboxControllerScheme
    {
        get
        {
            if (m_XboxControllerSchemeIndex == -1) m_XboxControllerSchemeIndex = asset.FindControlSchemeIndex("XboxController");
            return asset.controlSchemes[m_XboxControllerSchemeIndex];
        }
    }
    public interface IBubbleMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
