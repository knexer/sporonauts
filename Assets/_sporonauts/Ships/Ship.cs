using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles overall ship state, switching between inventory and control modes
public enum ShipState {
    Inventory,
    Control
}

public class Ship : MonoBehaviour
{
    [SerializeField] private CameraFocuser cameraFocuser;

    private ShipState state;
    private ShipInput input;

    private void Awake() {
        input = new ShipInput();
        state = ShipState.Control;
    }

    private void OnEnable() {
        input.Enable();
        // TODO some of this should maybe be in awake
        input.Flying.ActivateEngine.performed += OnActivateEngine;
        input.Flying.ActivateEngine.canceled += OnDeactivateEngine;
        input.Flying.SwitchToInventory.performed += OnSwitchToInventory;
        input.Inventory.SwitchToFlying.performed += OnSwitchToFlying;
        input.Flying.Enable();
        input.Inventory.Disable();
    }

    private void OnDisable() {
        input.Disable();
        // TODO some of this should maybe be in ondestroy
        input.Flying.ActivateEngine.performed -= OnActivateEngine;
        input.Flying.ActivateEngine.canceled += OnDeactivateEngine;
        input.Flying.SwitchToInventory.performed -= OnSwitchToInventory;
        input.Inventory.SwitchToFlying.performed -= OnSwitchToFlying;
    }

    private void OnActivateEngine(InputAction.CallbackContext context) {
        GetComponentInChildren<IShipComponent>().Activate();
    }

    private void OnDeactivateEngine(InputAction.CallbackContext context) {
        GetComponentInChildren<IShipComponent>().Deactivate();
    }

    private void OnSwitchToFlying(InputAction.CallbackContext context) {
        state = ShipState.Control;
        input.Flying.Enable();
        input.Inventory.Disable();
        cameraFocuser.Unfocus();
    }

    private void OnSwitchToInventory(InputAction.CallbackContext context) {
        state = ShipState.Inventory;
        input.Flying.Disable();
        input.Inventory.Enable();
        cameraFocuser.Focus(gameObject);
    }
}
