using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private float dragForce = 10f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxRange = 2f;
    [SerializeField] public Arm arm = null;

    private Camera mainCamera = null;
    private Rigidbody2D dragTarget = null;
    private Coroutine dragRoutine = null;

    private void Awake() {
        mainCamera = Camera.main;
        arm.SetRange(this, maxRange);
    }

    public void OnDragBegin(InputAction.CallbackContext context){

        Resource target = GetDragTarget();
        if (target == null) {
            return;
        }

        target.gameObject.layer = LayerMask.NameToLayer("Dragging");
        Inventory inventory = target.GetComponentInParent<Inventory>();
        if (inventory) {
            inventory.RemoveResource(target);
        }
        dragTarget = target.GetComponent<Rigidbody2D>();
        dragRoutine = StartCoroutine(DragTarget());
    }

    private Resource GetDragTarget() {
        // Convert the mouse position to world space and do a raycast to find the draggable object.
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 mousePositionWorld = mainCamera.ScreenToWorldPoint(mousePosition);

        if (Vector2.Distance(mousePositionWorld, transform.position) > maxRange) {
            return null;
        }

        LayerMask layerMask = LayerMask.GetMask("Draggable");
        RaycastHit2D hit = Physics2D.Raycast(mousePositionWorld, Vector2.zero, Mathf.Infinity, layerMask);
        if (!hit) {
            return null;
        }

        Resource target = hit.collider.GetComponent<Resource>();
        if (hit.collider.GetComponentInParent<ResourceDeposit>()){
            target = hit.collider.GetComponentInParent<ResourceDeposit>().MakeResource(mousePositionWorld).GetComponent<Resource>();
        }
        if (!target) {
            return null;
        }

        return target;
    }

    private IEnumerator DragTarget() {
        Vector3 velocity = Vector3.zero;
        while (true) {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Clamp the mouse position to the max range.
            Vector2 direction = worldMousePosition - (Vector2)transform.position;
            if (direction.magnitude > maxRange) {
                worldMousePosition = (Vector2)transform.position + direction.normalized * maxRange;
            }

            Vector2 force = (worldMousePosition - (Vector2)dragTarget.transform.position) * dragForce;
            dragTarget.velocity = Mathf.Min(force.magnitude, maxSpeed) * force.normalized;

            yield return new WaitForFixedUpdate();
        }
    }

    public void OnDrop(InputAction.CallbackContext context) {
        if (dragTarget == null) {
            return;
        }

        StopCoroutine(dragRoutine);
        dragRoutine = null;

        dragTarget.gameObject.layer = LayerMask.NameToLayer("Draggable");

        // Check for an inventory.
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 mousePositionWorld = mainCamera.ScreenToWorldPoint(mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePositionWorld, Vector2.zero);
        RaycastHit2D hit = System.Array.Find(hits, h => h.collider.GetComponent<Inventory>());
        if (hit)
        {
            hit.collider.GetComponent<Inventory>().AddResource(dragTarget.GetComponent<Resource>(), true);
        }

        dragTarget = null;
    }
}
