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
    [SerializeField] private CameraTarget cameraTarget;
    [SerializeField] private DragAndDrop dragAndDrop;
    [SerializeField] private EngineMount engineMount;
    [SerializeField] private ShipLocomotion locomotion;
    [SerializeField] private float dryMass;

    private ShipState state;
    private ShipInput input;

    private void Awake() {
        input = new ShipInput();
        state = ShipState.Control;
        foreach (Inventory inventory in GetComponentsInChildren<Inventory>()) {
            inventory.OnContentsChanged += UpdateMass;
        }
    }

    private void Start() {
        UpdateMass();
        StartCoroutine(EnableInputAfterDelay());
    }

    private IEnumerator EnableInputAfterDelay() {
        // Wait for 0.1 seconds to avoid input being triggered on the first frame,
        // particularly when hitting the play button in editor!
        yield return new WaitForSeconds(0.1f);
        EnableInput();
    }

    private void EnableInput() {
        input.Enable();
        input.Flying.ActivateEngine.performed += OnActivateEngine;
        input.Flying.ActivateEngine.canceled += OnDeactivateEngine;
        input.Flying.SwitchToInventory.performed += OnSwitchToInventory;
        input.Flying.OrientEngineMount.performed += engineMount.PointAtMouse;
        input.Inventory.SwitchToFlying.performed += OnSwitchToFlying;
        input.Inventory.Drag.performed += dragAndDrop.OnDragBegin;
        input.Inventory.Drag.canceled += dragAndDrop.OnDrop;
        input.Inventory.PositionHand.performed += dragAndDrop.arm.TrackMouse;
        input.Always.WalkClockwise.performed += locomotion.OnWalkClockwise;
        input.Always.WalkClockwise.canceled += locomotion.OnWalkClockwise;
        input.Always.WalkAntiClockwise.performed += locomotion.OnWalkAntiClockwise;
        input.Always.WalkAntiClockwise.canceled += locomotion.OnWalkAntiClockwise;
        OnSwitchToFlying(new InputAction.CallbackContext());
    }

    private void OnDisable() {
        input.Disable();
        input.Flying.ActivateEngine.performed -= OnActivateEngine;
        input.Flying.ActivateEngine.canceled += OnDeactivateEngine;
        input.Flying.SwitchToInventory.performed -= OnSwitchToInventory;
        input.Flying.OrientEngineMount.performed -= engineMount.PointAtMouse;
        input.Inventory.SwitchToFlying.performed -= OnSwitchToFlying;
        input.Inventory.Drag.performed -= dragAndDrop.OnDragBegin;
        input.Inventory.Drag.canceled -= dragAndDrop.OnDrop;
        input.Inventory.PositionHand.performed -= dragAndDrop.arm.TrackMouse;
        input.Always.WalkClockwise.performed -= locomotion.OnWalkClockwise;
        input.Always.WalkClockwise.canceled -= locomotion.OnWalkClockwise;
        input.Always.WalkAntiClockwise.performed -= locomotion.OnWalkAntiClockwise;
        input.Always.WalkAntiClockwise.canceled -= locomotion.OnWalkAntiClockwise;
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
        engineMount.Activate();
        dragAndDrop.arm.Deactivate();
    }

    private void OnSwitchToInventory(InputAction.CallbackContext context) {
        state = ShipState.Inventory;
        input.Flying.Disable();
        input.Inventory.Enable();
        cameraFocuser.Focus(cameraTarget);
        engineMount.Deactivate();
        dragAndDrop.arm.Activate();
    }

    private void UpdateMass() {
        float wetMass = dryMass;
        foreach(Inventory inventory in GetComponentsInChildren<Inventory>()) {
            wetMass += inventory.GetContentsMass();
        }
        GetComponent<Rigidbody2D>().mass = wetMass;
    }
}
