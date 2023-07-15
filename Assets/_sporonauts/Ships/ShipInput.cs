//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/_sporonauts/Ships/ShipInput.inputactions
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

public partial class @ShipInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShipInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ShipInput"",
    ""maps"": [
        {
            ""name"": ""Flying"",
            ""id"": ""7e106430-8cdf-4c2e-a028-fd2bba493a2a"",
            ""actions"": [
                {
                    ""name"": ""ActivateEngine"",
                    ""type"": ""Button"",
                    ""id"": ""884c90f7-467a-4db3-af53-dfaf2d898895"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchToInventory"",
                    ""type"": ""Button"",
                    ""id"": ""2497ef4b-b903-4d16-b6df-a8088338c38e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OrientEngineMount"",
                    ""type"": ""Value"",
                    ""id"": ""cf21d22d-0381-47ec-82d7-a17057ca5716"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ef0b928-b9e3-4636-b088-d7d4adc91a1f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActivateEngine"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0fce606-b994-4574-b5d2-ec3f745b237c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7981be3f-528d-4889-95b8-ba4ee7e855c0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OrientEngineMount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""2bc81ea1-86eb-4030-bfa2-a1ab72110791"",
            ""actions"": [
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""ae1ed454-4c41-4ed3-9792-38c13e3b25eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchToFlying"",
                    ""type"": ""Button"",
                    ""id"": ""83daa2fa-25e1-40d5-81cb-f8363c6224f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""05f96ad2-6868-406f-8121-bea29d245ee1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6051fdcc-d215-4a65-8793-555e6b72df1a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchToFlying"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Flying
        m_Flying = asset.FindActionMap("Flying", throwIfNotFound: true);
        m_Flying_ActivateEngine = m_Flying.FindAction("ActivateEngine", throwIfNotFound: true);
        m_Flying_SwitchToInventory = m_Flying.FindAction("SwitchToInventory", throwIfNotFound: true);
        m_Flying_OrientEngineMount = m_Flying.FindAction("OrientEngineMount", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Drag = m_Inventory.FindAction("Drag", throwIfNotFound: true);
        m_Inventory_SwitchToFlying = m_Inventory.FindAction("SwitchToFlying", throwIfNotFound: true);
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

    // Flying
    private readonly InputActionMap m_Flying;
    private List<IFlyingActions> m_FlyingActionsCallbackInterfaces = new List<IFlyingActions>();
    private readonly InputAction m_Flying_ActivateEngine;
    private readonly InputAction m_Flying_SwitchToInventory;
    private readonly InputAction m_Flying_OrientEngineMount;
    public struct FlyingActions
    {
        private @ShipInput m_Wrapper;
        public FlyingActions(@ShipInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ActivateEngine => m_Wrapper.m_Flying_ActivateEngine;
        public InputAction @SwitchToInventory => m_Wrapper.m_Flying_SwitchToInventory;
        public InputAction @OrientEngineMount => m_Wrapper.m_Flying_OrientEngineMount;
        public InputActionMap Get() { return m_Wrapper.m_Flying; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FlyingActions set) { return set.Get(); }
        public void AddCallbacks(IFlyingActions instance)
        {
            if (instance == null || m_Wrapper.m_FlyingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_FlyingActionsCallbackInterfaces.Add(instance);
            @ActivateEngine.started += instance.OnActivateEngine;
            @ActivateEngine.performed += instance.OnActivateEngine;
            @ActivateEngine.canceled += instance.OnActivateEngine;
            @SwitchToInventory.started += instance.OnSwitchToInventory;
            @SwitchToInventory.performed += instance.OnSwitchToInventory;
            @SwitchToInventory.canceled += instance.OnSwitchToInventory;
            @OrientEngineMount.started += instance.OnOrientEngineMount;
            @OrientEngineMount.performed += instance.OnOrientEngineMount;
            @OrientEngineMount.canceled += instance.OnOrientEngineMount;
        }

        private void UnregisterCallbacks(IFlyingActions instance)
        {
            @ActivateEngine.started -= instance.OnActivateEngine;
            @ActivateEngine.performed -= instance.OnActivateEngine;
            @ActivateEngine.canceled -= instance.OnActivateEngine;
            @SwitchToInventory.started -= instance.OnSwitchToInventory;
            @SwitchToInventory.performed -= instance.OnSwitchToInventory;
            @SwitchToInventory.canceled -= instance.OnSwitchToInventory;
            @OrientEngineMount.started -= instance.OnOrientEngineMount;
            @OrientEngineMount.performed -= instance.OnOrientEngineMount;
            @OrientEngineMount.canceled -= instance.OnOrientEngineMount;
        }

        public void RemoveCallbacks(IFlyingActions instance)
        {
            if (m_Wrapper.m_FlyingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IFlyingActions instance)
        {
            foreach (var item in m_Wrapper.m_FlyingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_FlyingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public FlyingActions @Flying => new FlyingActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private List<IInventoryActions> m_InventoryActionsCallbackInterfaces = new List<IInventoryActions>();
    private readonly InputAction m_Inventory_Drag;
    private readonly InputAction m_Inventory_SwitchToFlying;
    public struct InventoryActions
    {
        private @ShipInput m_Wrapper;
        public InventoryActions(@ShipInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Drag => m_Wrapper.m_Inventory_Drag;
        public InputAction @SwitchToFlying => m_Wrapper.m_Inventory_SwitchToFlying;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void AddCallbacks(IInventoryActions instance)
        {
            if (instance == null || m_Wrapper.m_InventoryActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Add(instance);
            @Drag.started += instance.OnDrag;
            @Drag.performed += instance.OnDrag;
            @Drag.canceled += instance.OnDrag;
            @SwitchToFlying.started += instance.OnSwitchToFlying;
            @SwitchToFlying.performed += instance.OnSwitchToFlying;
            @SwitchToFlying.canceled += instance.OnSwitchToFlying;
        }

        private void UnregisterCallbacks(IInventoryActions instance)
        {
            @Drag.started -= instance.OnDrag;
            @Drag.performed -= instance.OnDrag;
            @Drag.canceled -= instance.OnDrag;
            @SwitchToFlying.started -= instance.OnSwitchToFlying;
            @SwitchToFlying.performed -= instance.OnSwitchToFlying;
            @SwitchToFlying.canceled -= instance.OnSwitchToFlying;
        }

        public void RemoveCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInventoryActions instance)
        {
            foreach (var item in m_Wrapper.m_InventoryActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    public interface IFlyingActions
    {
        void OnActivateEngine(InputAction.CallbackContext context);
        void OnSwitchToInventory(InputAction.CallbackContext context);
        void OnOrientEngineMount(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnDrag(InputAction.CallbackContext context);
        void OnSwitchToFlying(InputAction.CallbackContext context);
    }
}
